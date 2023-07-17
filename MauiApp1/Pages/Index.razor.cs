

using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Net.Http.Json;

namespace MauiApp1.Pages
{
    public partial class Index

    {
        public List<Book> RetrievedBooks { get; set; } = new List<Book>();
        private string networkstatus;
        public Index()
        {
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)

            {

                networkstatus = "Connected";

            }
            else
            {

                networkstatus = "Not Connected";

            }

            StateHasChanged();
        }

       



        protected override async Task OnInitializedAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7112/Book");
            request.Headers.Add("accept", "application/json");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<Book>>();

            RetrievedBooks = result;

           
        }

        private void HandleSubmit()
        {
            bool isConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;
            if (isConnected)
            {
                ShowToast("Connected");
            }
            else
            {
                ShowToast("Not Connected");
            }
        }
        public async void ShowToast(string message)
        {

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();


            ToastDuration duration = ToastDuration.Short;

            double fontSize = 28;


            var toast = Toast.Make(message, duration, fontSize);


            await toast.Show(cancellationTokenSource.Token);

        }

    }
}
    


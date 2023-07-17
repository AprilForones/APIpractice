

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
    }
    
}

﻿

using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MauiApp1.LocalDB;
using MauiApp1.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace MauiApp1.Pages
{
    public partial class Index

    {
        [Inject]
        private IMyLocationService MyLocationService { get; set; }
        public List<Book> RetrievedBooks { get; set; } = new List<Book>();
        private string networkstatus;
        private string location;
        private List<MyLocation> Locations { get; set; } = new List<MyLocation>();
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
            //var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7112/Book");
            //request.Headers.Add("accept", "application/json");
            //var response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            //var result = await response.Content.ReadFromJsonAsync<List<Book>>();

            //RetrievedBooks = result;

        
            var getLocation = await GetLocationAsync();
            location = $"Latitude: {getLocation.Latitude}, Longitude: {getLocation.Longitude}, Altitude: {getLocation.Altitude}";
            Locations = await MyLocationService.GetMyLocations();
            StateHasChanged();
        }
        public async Task<Location> GetLocationAsync()
        {

            var location = await Geolocation.GetLocationAsync(new GeolocationRequest { DesiredAccuracy = GeolocationAccuracy.Best });
            await MyLocationService.AddMyLocation(location.Latitude, location.Longitude);
            return location;

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


            ToastDuration duration = ToastDuration.Long;

            double fontSize = 28;


            var toast = Toast.Make(message, duration, fontSize);


            await toast.Show(cancellationTokenSource.Token);

        }

    }
}
    


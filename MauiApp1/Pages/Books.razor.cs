using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;


namespace MauiApp1.Pages
{
    public partial class Books
    {
        public List<Book> RetrievedBooks { get; set; } = new List<Book>();
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
        private async Task HandSubmit()
        {
            Console.WriteLine();
        }
    }
}

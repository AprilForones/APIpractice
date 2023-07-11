using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;


namespace MauiApp1.Pages
{
    public partial class Authors
    {
        public List<Author> AuthorsList { get; set; } = new List<Author>();
        protected override async Task OnInitializedAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7112/Author");
            request.Headers.Add("accept", "application/json");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<Author>>();

            AuthorsList = result;


        }
    }
}
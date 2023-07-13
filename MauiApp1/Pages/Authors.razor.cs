using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace MauiApp1.Pages
{
    public partial class Authors
    {
        public List<Author> AuthorsList { get; set; } = new List<Author>();
        private string authorFName;
        private string authorLName;
        private string authorBirth;
       // private string AuthorId;
        protected override async Task OnInitializedAsync()
        {
            AuthorsList = await GetAuthorsAsync();


        }

        private async Task<List<Author>> GetAuthorsAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7112/Author");
            request.Headers.Add("accept", "application/json");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<Author>>();

            return result;

        }
        private StringContent CreateJsonContent(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async Task HandleSubmit()
        {
            var author = new Author();
            author.FName = authorFName;
            author.LName = authorFName;
            author.Birthdate = authorBirth;

            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7112/Author");
            request.Headers.Add("accept", "application/json");
            request.Content = CreateJsonContent(author);
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            // Check the response status code.
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // The book was added successfully.
                Console.WriteLine("Book added successfully");
              AuthorsList = await GetAuthorsAsync();
                StateHasChanged();
            }
            else
            {
                // The book was not added successfully.
                Console.WriteLine("Error adding book");
            }
            //Console.WriteLine("Book added successfully");
        }
    }
}
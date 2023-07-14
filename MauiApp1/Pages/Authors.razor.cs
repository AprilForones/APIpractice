
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
        private string AuthorId;
        private bool IsAdd = true;
        private string buttonText = "SUBMIT";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                AuthorsList = await GetAuthorsAsync();
                StateHasChanged();
            }
        }
        protected override async Task OnInitializedAsync()
        {



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
            if (IsAdd)
            {

                var author = new Author();
                author.FName = authorFName;
                author.LName = authorLName;
                author.Birthdate = authorBirth;
                author.ID = int.Parse(AuthorId);
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7112/Authors");
                request.Headers.Add("accept", "application/json");
                request.Content = CreateJsonContent(author);
                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();
                // Check the response status code.
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // The book was added successfully.
                    Console.WriteLine("Author added successfully");
                    //AuthorsList = await GetAuthorsAsync();
                    //StateHasChanged();
                }
                else
                {
                    // The book was not added successfully.
                    Console.WriteLine("Error adding Author");
                }
            }

            else
            {
                var author = new Author();
                author.ID = int.Parse(AuthorId);
                author.FName = authorFName;
                author.LName = authorLName;
                author.Birthdate = authorBirth;

                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7112/Authors");
                request.Headers.Add("accept", "application/json");
                request.Content = CreateJsonContent(author);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

            }
            IsAdd = true;
            buttonText = "SUBMIT";
            AuthorsList = await GetAuthorsAsync();
            StateHasChanged();



        }
        //Console.WriteLine("Book added successfully");

        private async void HandleDeleteAuthor(int authorId)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7112/Authors/{authorId}");
            request.Headers.Add("accept", "application/json");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            AuthorsList = await GetAuthorsAsync();
            StateHasChanged();
        }
        private async void HandleEditAuthor(Author b)
        {
            authorFName = b.FName;
            authorLName = b.LName;
            authorBirth = b.Birthdate;
            AuthorId = b.ID.ToString();
            IsAdd = false;
            buttonText = "UPDATE";

            StateHasChanged();
        }
    } 
}
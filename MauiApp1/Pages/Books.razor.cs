using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;


namespace MauiApp1.Pages
{
    public partial class Books
    {
        public List<Book> RetrievedBooks { get; set; } = new List<Book>();
        private string bookName;
        private string bookDescription;
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
        private StringContent CreateJsonContent(object data)
        {

            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");

        }
        private async Task HandSubmit()
        {
            var book = new Book();
            book.Name = bookName;
            book.Description = bookDescription;
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7112/Book");
            request.Headers.Add("accept", "application/json");
            request.Content = CreateJsonContent(book);
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            // Check the response status code.
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // The book was added successfully.
                Console.WriteLine("Book added successfully");
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

﻿using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace MauiApp1.Pages
{
    public partial class Books
    {
        public List<Book> RetrievedBooks { get; set; } = new List<Book>();
        private string bookName;
        private string AuthorId;
        private string bookDescription;
        private int bookId;
        private bool IsAdd = true;
        private string buttonText = "SUBMIT";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                RetrievedBooks = await GetBooksAsync();
                StateHasChanged();
            }
        }
  

        private async Task<List<Book>> GetBooksAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7112/Book");
            request.Headers.Add("accept", "application/json");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Book>>();
        }

        private StringContent CreateJsonContent(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async Task HandSubmit()
        {

            if (IsAdd)
            {
                var book = new Book();
                book.Name = bookName;
                book.Description = bookDescription;
                book.AuthorId = int.Parse(AuthorId);
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7112/Books");
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
            }
            else
            {
                var book = new Book();
                book.ID = bookId;
                book.Name = bookName;
                book.Description = bookDescription;
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7112/Books");
                request.Headers.Add("accept", "application/json");
                request.Content = CreateJsonContent(book);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
         
            }
            IsAdd = true;
            buttonText = "SUBMIT";
            RetrievedBooks = await GetBooksAsync();
            StateHasChanged();
            //Console.WriteLine("Book added successfully");
        }
        private async void HandleDeleteBook(int bookId)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7112/Books/{bookId}");
            request.Headers.Add("accept", "application/json");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            RetrievedBooks = await GetBooksAsync();
            StateHasChanged();
        }
        private  void HandleEditBook(Book b)
        {
            bookDescription = b.Description;
            bookName = b.Name;
            bookId = b.ID;
            AuthorId = b.AuthorId.HasValue ? b.AuthorId.Value.ToString() : "";
            IsAdd = false;
            buttonText = "UPDATE";

            StateHasChanged();
        }
    }
}
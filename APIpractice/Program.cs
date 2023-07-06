using APIpractice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BooksDbContext>(options =>
    options.UseMySQL("Server=localhost,3306;Database=booksdb;Uid=root;Pwd=aries@041300;"));

builder.Services.AddDbContext<BooksDbContext>(options =>
    options.UseMySQL("Server=localhost,3306;Database=booksdb;Uid=root;Pwd=root;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");


app.MapPost("/Books", ([FromBody] Book book, BooksDbContext db) =>
{
    db.Add(book);
    db.SaveChanges();
    return book;
});
app.MapPut("/Books", ([FromBody] Book book, BooksDbContext db) => {

    var b = db.Books.Where(c => c.ID == book.ID).FirstOrDefault();

    b.Name = book.Name;

   // b.Name = book.Name;
//  b.Description = book.Description;

    db.SaveChanges();

    return b;

});
app.MapDelete("/Books/{id}", ([FromRoute] int id, BooksDbContext db) =>
{
    var b = db.Books.Find(id);
    db.Books.Remove(b);
    db.SaveChanges();

});
app.MapGet("/Books/{id}", ([FromRoute] int id, BooksDbContext db) =>
{
    var b = db.Books.Find(id);
    return b;

});
app.MapGet("/Books", (BooksDbContext db) =>
{
   

    return db.Books.ToList();


}).WithName("GetBooks");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


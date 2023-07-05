using APIpractice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BooksDbContext>(options =>
    options.UseMySQL("Server=localhost,3306;Database=booksdb;Uid=root;Pwd=aries@041300;"));

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



app.MapPost("/Books", ([FromBody] Book book) =>
{
   // books.Add(book);
    return book;
});

app.MapPut("/Books", ([FromBody] Book book) => {

   // var b = books.Where(c => c.ID == book.ID).FirstOrDefault();

   // b.Name = book.Name;
//  b.Description = book.Description;

    //return b;

});
app.MapDelete("/Books/{id}", ([FromRoute] int id) =>
{

   // var b = books.Where(c => c.ID == id).FirstOrDefault();

   // books.Remove(b);

});
app.MapGet("/Books", () =>
{
   

   // return books;


}).WithName("GetBooks");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


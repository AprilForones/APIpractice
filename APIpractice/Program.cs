using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

List<Book> books = new List<Book>();
books.Add(new Book
{
    Description = "Fantasy",
    Name = "Cinderella",
    ID = 1
});
books.Add(new Book
{
    Description = "SciFi",
    Name = "April",
    ID = 2
});
books.Add(new Book
{
    Description = "Comedy",
    Name = "Gintama",
    ID = 3
});

app.MapPost("/Books", ([FromBody] Book book) =>
{
    books.Add(book);
    return book;
});

app.MapPut("/Books", ([FromBody] Book book) => {

    var b = books.Where(c => c.ID == book.ID).FirstOrDefault();

    b.Name = book.Name;

    b.Description = book.Description;

    return b;

});
app.MapDelete("/Books/{id}", ([FromRoute] int id) =>
{

    var b = books.Where(c => c.ID == id).FirstOrDefault();

    books.Remove(b);

});
app.MapGet("/Books", () =>
{
   

    return books;


}).WithName("GetBooks");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class Book
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

}
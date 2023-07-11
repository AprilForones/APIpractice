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


app.MapPost("/Books", ([FromBody] Book book, BooksDbContext db) =>
{
    db.Add(book);
    db.SaveChanges();
    return book;
});
app.MapPut("/Books", ([FromBody] Book book, BooksDbContext db) => {

    var b = db.Books.Where(c => c.ID == book.ID).FirstOrDefault();

    b.Name = book.Name;

    b.Name = book.Name;
    b.Description = book.Description;

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
    var b = db.Books.Include(c => c.Author)

.Select(c => new
{


    ID = c.ID,


    Name = c.Name,


    Description = c.Description,


    Author = new { c.Author.ID, c.Author.FName, c.Author.LName }


}).Where(c => c.ID == id).FirstOrDefault();

    return b;

});
app.MapGet("/Book", (BooksDbContext db) =>
{
   

    return db.Books.ToList();


}).WithName("GetBooks");

app.MapPost("/Authors", ([FromBody] Author author, BooksDbContext db) =>
{
    db.Add(author);
    db.SaveChanges();
    return author;
});
app.MapPut("/Authors", ([FromBody] Author author, BooksDbContext db) => {

    var b = db.Authors.Where(c => c.ID == author.ID).FirstOrDefault();

    b.FName = author.FName;

    b.LName = author.LName;
    b.Birthdate = author.Birthdate;

    db.SaveChanges();

    return b;

});
app.MapDelete("/Authors/{id}", ([FromRoute] int id, BooksDbContext db) =>
{
    var b = db.Authors.Find(id);
    db.Authors.Remove(b);
    db.SaveChanges();

});
app.MapGet("/Authors/{id}", ([FromRoute] int id, BooksDbContext db) =>
{
    // var b = db.Authors.Include(c => c.Book).Where(c => c.ID == id).ToList(); ;

    // var b = db.Books.Include(c => c.Author).Where(c => c.ID == id).ToList();


    //var authors = db.Authors.ToList();
    // foreach (var author in authors)
    // {
    //     author.Books = db.Books.Where(b => b.AuthorId == id).ToList();
    // }

    //var b = db.Books.Include(c => c.Author).Where(c => c.ID == id).ToList();
    return db.Authors.Include(c => c.Books).Where(c => c.ID == id)

        .Select(c => new {

                        c.ID,
                        c.FName,
                        c.LName,
                        c.Birthdate,
          Books = c.Books.Select(b => new { b.ID, b.Name, b.Description }).ToList() }).FirstOrDefault();



    

});

app.MapGet("/Author", (BooksDbContext db) =>
{

    return db.Authors.Include(c => c.Books)

.Select(c => new {

    c.ID,

    c.FName,

    c.LName,

    c.Birthdate,

    Books = c.Books.Select(b => new { BookID = b.ID, b.Name, b.Description }).ToList()

})

    .ToList();


}).WithName("GetAuthors");





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

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


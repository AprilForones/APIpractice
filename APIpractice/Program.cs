using APIpractice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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


app.MapPost("/Books", ([FromBody] AddBookRequest addBook, BooksDbContext db) =>
{
    if (!addBook.AuthorId.HasValue)
    {
        return Results.BadRequest("author must be supplied");
    }

    var authorInDb = db.Authors.Find(addBook.AuthorId);
    if (authorInDb == null)
    {
        return Results.NotFound("Author Not Found");
    }

    var bookToSave = new Book
    {
        Name = addBook.Name,
        Description = addBook.Description,
        AuthorId = addBook.AuthorId
    };

    try
    {
        db.Books.Add(bookToSave);
        db.SaveChanges();
    }
    catch (Exception)
    {

        return Results.StatusCode(500);
    }


    var response = new GetBookResponse
    {
        ID = bookToSave.ID,
        Description = bookToSave.Description,
        Name = bookToSave.Name,
        Author = new GetAuthor
        {
            ID = bookToSave.AuthorId.Value,
            FName = bookToSave.Author.FName,
            LName = bookToSave.Author.LName
        }
    };

    return Results.Ok(response);
});

app.MapPut("/Books", ([FromBody] EditBookRequest editBook, BooksDbContext db) => {

    var b = db.Books.Where(c => c.ID == editBook.Id).FirstOrDefault();

    if(b == null)
    {
        return Results.NotFound();
    }

    b.Name = editBook.Name;

    b.Description = editBook.Description;

    db.SaveChanges();


    return Results.Ok(b);

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

.Select(c => new GetBookResponse
{
    ID = c.ID,
    Name = c.Name,
    Description = c.Description,
    Author = new GetAuthor { 
        ID =  c.Author.ID, 
        FName =  c.Author.FName, 
        LName =  c.Author.LName 
    }
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


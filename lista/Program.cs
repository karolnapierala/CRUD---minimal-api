using lista;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();
IDbBookService dbBookService = new DbBookService();

dbBookService.ReadBooksDb();
app.MapGet("/books", () =>
{
    List<Book> books = dbBookService.GetAll();
    JsonConvert.SerializeObject(books);
    return Results.Ok(books);
});
app.MapGet("/books/{id}", (int id) =>
{
   Book book = dbBookService.GetById(id);
   JsonConvert.SerializeObject(book);
   return Results.Ok(book);
});
app.MapGet("/books/{title}", (string title) =>
{
    List<Book> books = dbBookService.Get(title);
    JsonConvert.SerializeObject(books);
    return Results.Ok(books);
});
app.MapPost("/books", (Book book) =>
{
    dbBookService.Add(book);
    var createdBook = dbBookService.GetLast();
    JsonConvert.SerializeObject(createdBook);
    return Results.Created($"/books/{createdBook.Id}", createdBook);
});
app.MapDelete("/books/{id}", (int id) =>
{
    dbBookService.Delete(id);
    dbBookService.DeleteInDb(id);
    return Results.Ok();
});
app.MapPut("/books/{id}", (int id, Book book) =>
{ 
    book.Id = id;
    dbBookService.Update(id, book);
    return Results.NoContent();
});

app.Run();
using BookStore.API.Contracts;
using BookStore.Core.Abstractions;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController: ControllerBase
{
    private readonly IBooksService booksService;

    public BooksController(IBooksService booksService)
    {
        this.booksService = booksService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BookResponse>>> GetBooks()
    {
        var books = await booksService.GetAllBooks();

        var response = books.Select(b => new BookResponse(b.Id, b.Title, b.Description, b.Price));

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateBook([FromBody] BookRequest request)
    {
        var (book, error) = Book.Create(
            Guid.NewGuid(),
            request.Title,
            request.Description,
            request.Price);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        var bookId = await booksService.CreateBook(book);
        
        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateBook(Guid id, [FromBody] BookRequest request)
    {
        var bookId = await booksService.UpdateBook(id, request.Title, request.Description, request.Price);

        return Ok(bookId);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteBook(Guid id)
    {
        return Ok(await booksService.DeleteBook(id));
    }
}
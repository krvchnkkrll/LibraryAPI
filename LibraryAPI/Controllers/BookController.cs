using LibraryAPI.Handlers.Books.Commands;
using LibraryAPI.Handlers.Books.Commands.Create;
using LibraryAPI.Handlers.Books.Commands.Delete;
using LibraryAPI.Handlers.Books.Commands.Update;
using LibraryAPI.Handlers.Books.Queries;
using LibraryAPI.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public BookController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks(
        [FromQuery] string? name,
        [FromQuery] Genre? genre,
        [FromQuery] BookStatus? bookStatus,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        CancellationToken cancellationToken)
    {
        var query = new GetAllBooks(
            name,
            genre,
            bookStatus,
            pageNumber ?? 1,
            pageSize ?? 10);

        var books = await _mediator.Send(query, cancellationToken);

        return Ok(books);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(
        [FromBody] BookCommandDto dto,
        CancellationToken cancellationToken)
    {
        var book = new CreateBook(dto);

        await _mediator.Send(book, cancellationToken);

        return Ok("Книга добавлена");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBook(
        int id,
        [FromBody] BookCommandDto dto,
        CancellationToken cancellationToken)
    {
        var book = new UpdateBook(id, dto);

        await _mediator.Send(book, cancellationToken);

        return Ok("Книга измена");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBook(
        int id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteBook(id), cancellationToken);

        return NoContent();
    }
    
}
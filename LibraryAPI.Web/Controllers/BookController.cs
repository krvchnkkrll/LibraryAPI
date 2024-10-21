using LibraryAPI.Application.Handlers.Books.Commands;
using LibraryAPI.Application.Handlers.Books.Commands.Create;
using LibraryAPI.Application.Handlers.Books.Commands.Delete;
using LibraryAPI.Application.Handlers.Books.Commands.Update;
using LibraryAPI.Application.Handlers.Books.Queries.GetAllBooks;
using LibraryAPI.Application.Handlers.Books.Queries.GetBookById;
using LibraryAPI.Core.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public BookController(IMediator mediator, ILogger<BookController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("GetAllBooks")]
    public async Task<IActionResult> GetAllBooks(
        [FromQuery] string? name,
        [FromQuery] Genre? genre,
        [FromQuery] BookStatus? bookStatus,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        CancellationToken cancellationToken)
    {
        try
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
        
        catch (Exception)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("GetAllBooksSQL")]
    public async Task<IActionResult> GetAllBooksSql(
        CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetAllBooksSql();

            var books = await _mediator.Send(query, cancellationToken);
       
            return Ok(books);
        }
        
        catch (Exception)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
    }

    [HttpGet("GetBookById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBookById(
        [FromQuery] int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetBookById(id);

            var book = await _mediator.Send(query, cancellationToken);
            
            return Ok(book);
        }
        
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        
        catch (Exception)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
    }

    
    [HttpPost("CreateBook")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    //[Authorize(Policy = "IsItStaff")]
    public async Task<IActionResult> CreateBook(
        [FromBody] BookCommandDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var book = new CreateBook(dto);

            await _mediator.Send(book, cancellationToken);

            return Ok("Книга добавлена");
        }
        
        catch (Exception)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
        
    }

    [HttpPut("UpdateBook")]
    //[Authorize(Policy = "IsItStaff")]
    public async Task<IActionResult> UpdateBook(
        int id,
        [FromBody] BookCommandDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var book = new UpdateBook(id, dto);

            await _mediator.Send(book, cancellationToken);

            return Ok("Книга измена");
        }
        
        catch (Exception)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
    }

    
    [HttpDelete("DeleteBook")]
    //[Authorize(Policy = "IsItStaff")]
    public async Task<IActionResult> DeleteBook(
        int id,
        CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteBook(id), cancellationToken);
            return Ok("Книга удалена");
        }
        
        catch (KeyNotFoundException)
        {
            return NotFound("Запись не найдена");
        }
        
        catch (Exception)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
    }
    
}
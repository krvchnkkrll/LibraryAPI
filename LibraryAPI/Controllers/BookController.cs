using LibraryAPI.Handlers.Books.Commands;
using LibraryAPI.Handlers.Books.Commands.Create;
using LibraryAPI.Handlers.Books.Commands.Delete;
using LibraryAPI.Handlers.Books.Commands.Update;
using LibraryAPI.Handlers.Books.Queries;
using LibraryAPI.Handlers.Books.Queries.GetAllBooks;
using LibraryAPI.Handlers.Books.Queries.GetBookById;
using LibraryAPI.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BookController> _logger;
    
    public BookController(IMediator mediator, ILogger<BookController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        catch (Exception e)
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
        
        catch (Exception e)
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
        catch (Exception e)
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
        catch (Exception e)
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
        
        catch (Exception e)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
    }
    
}
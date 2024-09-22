using LibraryAPI.Application.Handlers.UserBooks.Commands;
using LibraryAPI.Application.Handlers.UserBooks.Commands.Create;
using LibraryAPI.Application.Handlers.UserBooks.Commands.Delete;
using LibraryAPI.Application.Handlers.UserBooks.Commands.Update;
using LibraryAPI.Application.Handlers.UserBooks.Queries.GetAllUsersBooks;
using LibraryAPI.Application.Handlers.UserBooks.Queries.GetUserBookById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserBookController : ControllerBase
{ 
    private readonly IMediator _mediator;

    public UserBookController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("GetAllUsersBooks")]
    public async Task<IActionResult> GetAllUsersBook(
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize, 
        CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetAllUserBooks(
                pageNumber ?? 1,
                pageSize ?? 10);

            var usersBook = await _mediator.Send(query, cancellationToken);

            return Ok(usersBook);
        }
        catch (Exception)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
    }

    [HttpGet("GetUserBookById")]
    public async Task<IActionResult> GetUserBookById(
        [FromQuery] int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetUserBookById(id);

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
    
    [HttpPost("CreateUserBook")]
    //[Authorize(Policy = "IsItStaff")]
    public async Task<IActionResult> CreateUsersBook(
        [FromBody] UserBookCommandDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var userBook = new CreateUserBooks(dto);

            await _mediator.Send(userBook, cancellationToken);

            return Ok("Книга пользователя добавлена");
        }
        catch (Exception)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
    }

    [HttpPut("UpdateUserBook")]
    //[Authorize(Policy = "IsItStaff")]
    public async Task<IActionResult> UpdateUserBook(
        int id,
        [FromBody] UserBookCommandDto dto,
        CancellationToken cancellationToken) 
    {
        try
        {
            var userBook = new UpdateUserBook(id, dto);

            await _mediator.Send(userBook, cancellationToken);

            return Ok("Книга пользователя изменена");
        }
        catch (Exception)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
    }

    [HttpDelete("DeleteUserBook")]
    //[Authorize(Policy = "IsItStaff")]
    public async Task<IActionResult> DeleteUserBook(
        int id,
        CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteUserBook(id), cancellationToken);

            return Ok("Книга ползоваеля удалена");
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
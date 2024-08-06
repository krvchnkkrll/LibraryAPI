using LibraryAPI.Handlers.UserBooks.Commands;
using LibraryAPI.Handlers.UserBooks.Commands.Create;
using LibraryAPI.Handlers.UserBooks.Commands.Delete;
using LibraryAPI.Handlers.UserBooks.Commands.Update;
using LibraryAPI.Handlers.UserBooks.Queries;
using LibraryAPI.Handlers.UserBooks.Queries.GetAllUserBooks;
using LibraryAPI.Handlers.UserBooks.Queries.GetUserBookById;
using LibraryAPI.Handlers.Users.Queries.GetUserById;
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
        var query = new GetAllUserBooks(
            pageNumber ?? 1,
            pageSize ?? 10);

        var usersBook = await _mediator.Send(query, cancellationToken);

        return Ok(usersBook);
    }

    [HttpGet("GetUserBookById")]
    public async Task<IActionResult> GetUserBookById(
        [FromQuery] int id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserBookById(id);

        var book = await _mediator.Send(query, cancellationToken);

        return Ok(book);
    }
    
    [HttpPost("CreateUserBook")]
    //[Authorize(Policy = "IsItStaff")]
    public async Task<IActionResult> CreateUsersBook(
        [FromBody] UserBookCommandDto dto,
        CancellationToken cancellationToken)
    {
        var userBook = new CreateUserBooks(dto);

        await _mediator.Send(userBook, cancellationToken);

        return Ok("Книга пользователя добавлена");
    }

    [HttpPut("UpdateUserBook")]
    //[Authorize(Policy = "IsItStaff")]
    public async Task<IActionResult> UpdateUserBook(
        int id,
        [FromBody] UserBookCommandDto dto,
        CancellationToken cancellationToken) {
        var userBook = new UpdateUserBook(id, dto);

        await _mediator.Send(userBook, cancellationToken);

        return Ok("Книга пользователя изменена");
    }

    [HttpDelete("DeleteUserBook")]
    //[Authorize(Policy = "IsItStaff")]
    public async Task<IActionResult> DeleteUserBook(
        int id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteUserBook(id), cancellationToken);

        return NoContent();
    }
}
using LibraryAPI.Handlers.UserBooks.Commands;
using LibraryAPI.Handlers.UserBooks.Commands.Create;
using LibraryAPI.Handlers.UserBooks.Commands.Delete;
using LibraryAPI.Handlers.UserBooks.Commands.Update;
using LibraryAPI.Handlers.UserBooks.Queries;
using MediatR;
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

    [HttpGet]
    public async Task<IActionResult> GetAllUsersBook(
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize, 
        CancellationToken cancellationToken)
    {
        var query = new GetAllUserBooks(
            pageNumber ?? 1,
            pageSize ?? 10);

        var usersbook = await _mediator.Send(query, cancellationToken);

        return Ok(usersbook);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUsersBook(
        [FromBody] UserBookCommandDto dto,
        CancellationToken cancellationToken)
    {
        var userbook = new CreateUserBooks(dto);

        await _mediator.Send(userbook, cancellationToken);

        return Ok("Книга пользователя добавлена");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserBook(
        int id,
        [FromBody] UserBookCommandDto dto,
        CancellationToken cancellationToken)
    {
        var userbook = new UpdateUserBook(id, dto);

        await _mediator.Send(userbook, cancellationToken);

        return Ok("Книга пользователя изменена");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserBook(
        int id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteUserBook(id), cancellationToken);

        return NoContent();
    }
}
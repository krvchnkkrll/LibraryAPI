using LibraryAPI.Handlers.Books.Queries;
using LibraryAPI.Handlers.Users.Commands;
using LibraryAPI.Handlers.Users.Commands.Create;
using LibraryAPI.Handlers.Users.Commands.Delete;
using LibraryAPI.Handlers.Users.Commands.Update;
using LibraryAPI.Handlers.Users.Queries;
using LibraryAPI.Handlers.Users.Queries.GetAllUsers;
using LibraryAPI.Handlers.Users.Queries.GetUserById;
using LibraryAPI.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("GetAllUsers")]
    [Authorize(Policy = "IsItStaff")]
    public async Task<IActionResult> GetAllUsers(
        [FromQuery] string? surname, 
        [FromQuery] string? name, 
        [FromQuery] string? patronymic,
        [FromQuery] bool? isItStaff,
        [FromQuery] string? searchQuery, 
        [FromQuery] int? pageNumber, 
        [FromQuery] int? pageSize,
        CancellationToken cancellationToken)
    {
        
        var query = new GetAllUsers(
            surname,
            name,
            patronymic,
            isItStaff,
            searchQuery,
            pageNumber ?? 1, 
            pageSize ?? 10
        );
        
        var user = await _mediator.Send(query, cancellationToken);
        
        return Ok(user);
    }

    [HttpGet("GetUserById")]
    public async Task<IActionResult> GetUserById(
        [FromQuery] int id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserById(id);

        var user = await _mediator.Send(query, cancellationToken);

        return Ok(user);
    }
    
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser(
        [FromBody] UserCommandDto dto,
        CancellationToken cancellationToken)
    {
        var user = new CreateUser(dto);

        await _mediator.Send(user, cancellationToken);

        return Ok("Пользователь добавлен");
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser(
        int id,
        [FromBody] UserCommandDto dto,
        CancellationToken cancellationToken)
    {
        var user = new UpdateUser(id, dto);

        await _mediator.Send(user, cancellationToken);

        return Ok("Пользователь изменен");
    }

    [Authorize(Policy = "IsItStaff")]
    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteUser(id), cancellationToken);

        return NoContent();
    }
}
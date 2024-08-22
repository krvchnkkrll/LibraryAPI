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
    //[Authorize(Policy = "IsItStaff")]
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
        try
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
        
        catch (Exception e)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
    }

    [HttpGet("GetUserById")]
    public async Task<IActionResult> GetUserById(
        [FromQuery] int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetUserById(id);

            var user = await _mediator.Send(query, cancellationToken);

            return Ok(user);
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
    
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser(
        [FromBody] UserCommandDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = new CreateUser(dto);

            await _mediator.Send(user, cancellationToken);

            return Ok("Пользователь добавлен");
        }
        
        catch (Exception e)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser(
        int id,
        [FromBody] UserCommandDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = new UpdateUser(id, dto);

            await _mediator.Send(user, cancellationToken);

            return Ok("Пользователь изменен");
        }
        
        catch (Exception e)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
    }

    //[Authorize(Policy = "IsItStaff")]
    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteUser(id), cancellationToken);

            return Ok("Пользоваель удален");
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
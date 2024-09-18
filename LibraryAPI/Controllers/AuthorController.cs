using LibraryAPI.Handlers.Authors.Commands;
using LibraryAPI.Handlers.Authors.Commands.Create;
using LibraryAPI.Handlers.Authors.Commands.Delete;
using LibraryAPI.Handlers.Authors.Commands.Update;
using LibraryAPI.Handlers.Authors.Queries;
using LibraryAPI.Handlers.Authors.Queries.GetAllAuthors;
using LibraryAPI.Handlers.Authors.Queries.GetAuthorById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("GetAllAuthors")]
    public async Task<IActionResult> GetAllAuthors(
        [FromQuery] string? surname,
        [FromQuery] string? name,
        [FromQuery] string? patronymic,
        [FromQuery] string? searchQuery,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetAllAuthors(
                surname,
                name,
                patronymic,
                searchQuery,
                pageNumber ?? 1,
                pageSize ?? 10
            );
        
            var authors = await _mediator.Send(query, cancellationToken);
        
            return Ok(authors);
        }
        catch (Exception)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
    }

    [HttpGet("GetAuthorById")]
    public async Task<IActionResult> GetAuthorById(
        [FromQuery] int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetAuthorById(id);

            var author = await _mediator.Send(query, cancellationToken);

            return Ok(author);
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
    
    [HttpPost("CreateAuthor")]
    //[Authorize(Policy = "IsItStaff")]
    public async Task<IActionResult> CreateAuthor(
        [FromBody] AuthorCommandDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var author = new CreateAuthor(dto);

            await _mediator.Send(author, cancellationToken);

            return Ok("Автор добавлен");
        }
        catch (Exception)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
    }

    [HttpPut("UpdateAuthor")]
    //[Authorize(Policy = "IsItStaff")]
    public async Task<IActionResult> UpdateAuthor(
        int id, 
        [FromBody] AuthorCommandDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var author = new UpdateAuthor(id, dto);

            await _mediator.Send(author, cancellationToken);

            return Ok("Автор изменен");
        }
        catch (Exception)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
    }

    //[Authorize(Policy = "IsItStaff")]
    [HttpDelete("DeleteAuthor")]
    public async Task<IActionResult> DeleteAuthor(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteAuthor(id), cancellationToken);
        
            return Ok("Автор удален");
        }
        catch (Exception)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса.");
        }
    }
}

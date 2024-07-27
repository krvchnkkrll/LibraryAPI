using LibraryAPI.Handlers.Authors.Commands;
using LibraryAPI.Handlers.Authors.Commands.Create;
using LibraryAPI.Handlers.Authors.Commands.Delete;
using LibraryAPI.Handlers.Authors.Commands.Update;
using LibraryAPI.Handlers.Authors.Queries;
using MediatR;
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

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors(
        [FromQuery] string? surname,
        [FromQuery] string? name,
        [FromQuery] string? patronymic,
        [FromQuery] string? searchQuery,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        CancellationToken cancellationToken)
    {
        var query = new GetAllAuthors(
            surname,
            name,
            patronymic,
            searchQuery,
            pageNumber ?? 1,
            pageSize ?? 10
        );
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor(
        [FromBody] AuthorCommandDto dto,
        CancellationToken cancellationToken)
    {
        var author = new CreateAuthor(dto);

        await _mediator.Send(author, cancellationToken);

        return Ok("Автор добавлен");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAuthor(
        int id, 
        [FromBody] AuthorCommandDto dto,
        CancellationToken cancellationToken)
    {
        var author = new UpdateAuthor(id, dto);

        await _mediator.Send(author, cancellationToken);

        return Ok("Автор изменен");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAuthor(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteAuthor(id), cancellationToken);
        
        return NoContent();
    }
}

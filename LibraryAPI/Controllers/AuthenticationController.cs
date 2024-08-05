using LibraryAPI.Handlers.Users.Commands.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpPost]
    public async Task<IActionResult> Authenticate(
        [FromBody] AuthenticationUsersDto dto,
        CancellationToken cancellationToken)
    {
        var user = new AuthenticationUsers(dto);
        var token = await _mediator.Send(user, cancellationToken);

        if (token == null)
        {
            return Unauthorized("Неправильный логин или пароль");
        }

        return Ok(token);
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryAPI.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LibraryAPI.Handlers.Users.Commands.Authentication;

public record AuthenticationUsers(AuthenticationUsersDto Dto) : IRequest<string>;

public sealed class AuthenticationUsersHandler : IRequestHandler<AuthenticationUsers, string>
{
    private readonly LibraryInfoContext _context;
    private readonly IConfiguration _configuration;

    public AuthenticationUsersHandler(LibraryInfoContext context, IConfiguration configuration)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<string> Handle(AuthenticationUsers request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(
            u => u.Login == request.Dto.Login && u.Password == request.Dto.Password, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException("Неправильный логин или пароль пользователя");
        }

        var secretKey = _configuration["Authentication:SecretForKey"] 
                        ?? throw new InvalidOperationException("Ключ отсутствует");

        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claimsForToken = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Surname, user.Surname),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.GivenName, user.Patronymic ?? string.Empty),
            new Claim(ClaimTypes.Role, user.IsItStaff.ToString())
        };

        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claimsForToken,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signingCredentials);

        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return tokenToReturn;
    }
}
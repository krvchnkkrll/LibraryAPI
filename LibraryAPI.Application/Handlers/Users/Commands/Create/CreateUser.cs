using LibraryAPI.Core.Models.Entities;
using LibraryAPI.Infrastructure.DbContext;
using MediatR;

namespace LibraryAPI.Application.Handlers.Users.Commands.Create;

public record CreateUser(UserCommandDto Dto): IRequest;

file sealed class CreateUserHandler : IRequestHandler<CreateUser>
{
    private readonly LibraryInfoContext _context;
    
    public CreateUserHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(CreateUser request, CancellationToken cancellationToken)
    {
        var user = new User
        (
            request.Dto.Surname,
            request.Dto.Name, 
            request.Dto.Patronymic,
            request.Dto.Login,
            request.Dto.Password,
            request.Dto.IsItStaff
        );

        await _context.Users.AddAsync(user, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
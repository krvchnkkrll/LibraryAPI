using LibraryAPI.Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Application.Handlers.Users.Commands.Delete;

public record DeleteUser(int Id) : IRequest;

file sealed class DeleteUserHandler : IRequestHandler<DeleteUser>
{
    private readonly LibraryInfoContext _context;

    public DeleteUserHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstAsync(
            u => u.Id == request.Id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"Пользователь с индексом {request.Id} не найден");
        }

        _context.Users.Remove(user);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
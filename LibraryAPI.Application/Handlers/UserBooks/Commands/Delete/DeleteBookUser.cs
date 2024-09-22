using LibraryAPI.Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Application.Handlers.UserBooks.Commands.Delete;

public record DeleteUserBook(int Id) : IRequest;

file sealed class DeleteUserBookHandler : IRequestHandler<DeleteUserBook>
{
    private readonly LibraryInfoContext _context;

    public DeleteUserBookHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(DeleteUserBook request, CancellationToken cancellationToken)
    {
        var userBook = await _context.UserBooks.FirstAsync(
            u => u.Id == request.Id, cancellationToken);

        if (userBook == null)
        {
            throw new KeyNotFoundException($"Запись о книге пользователя с индексом {request.Id} не найден");
        }

        _context.UserBooks.Remove(userBook);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
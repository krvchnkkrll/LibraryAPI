using LibraryAPI.Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Application.Handlers.Authors.Commands.Delete;

public record DeleteAuthor(int Id): IRequest;

file sealed class DeleteAuthorHandler : IRequestHandler<DeleteAuthor>
{
    private readonly LibraryInfoContext _context;

    public DeleteAuthorHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(DeleteAuthor request, CancellationToken cancellationToken)
    {
        var author = await _context.Authors.FirstAsync(
            a => a.Id == request.Id, cancellationToken);

        if (author == null)
        {
            throw new KeyNotFoundException($"Автор с индексом {request.Id} не найден.");
        }
        
        _context.Authors.Remove(author);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
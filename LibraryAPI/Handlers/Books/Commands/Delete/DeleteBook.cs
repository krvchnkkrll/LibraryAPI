using LibraryAPI.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Handlers.Books.Commands.Delete;

public record DeleteBook(int Id): IRequest;

file sealed class DeleteBookHandler : IRequestHandler<DeleteBook>
{
    private readonly LibraryInfoContext _context;

    public DeleteBookHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(DeleteBook request, CancellationToken cancellationToken)
    {
        var book = await _context.Books.FirstAsync(
            b => b.Id == request.Id, cancellationToken);

        if (book == null)
        {
            throw new KeyNotFoundException($"Книга с индексом {request.Id} не найден.");
        }

        _context.Books.Remove(book);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
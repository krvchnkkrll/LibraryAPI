using LibraryAPI.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Handlers.Books.Commands.Update;

public record UpdateBook(int Id, BookCommandDto Dto) : IRequest;

file sealed class UpdateBookHandler : IRequest<UpdateBook>
{
    private readonly LibraryInfoContext _context;

    public UpdateBookHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(UpdateBook request, CancellationToken cancellationToken)
    {
        var book = await _context.Books.FirstOrDefaultAsync(
            b => b.Id == request.Id, cancellationToken);

        if (book == null)
        {
            throw new KeyNotFoundException($"Книга с индексом {request.Id} не найдена");
        }

        book.Name = request.Dto.Name;
        book.BookStatus = request.Dto.BookStatus;
        book.Genre = request.Dto.Genre;

        _context.Books.Update(book);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
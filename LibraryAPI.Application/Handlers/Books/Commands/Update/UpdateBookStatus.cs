using LibraryAPI.Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Application.Handlers.Books.Commands.Update;

public record UpdateBookStatus(int Id, BookStatusCommandDto Dto) : IRequest;

file sealed class UpdateBookStatusHandler : IRequestHandler<UpdateBookStatus>
{
    private readonly LibraryInfoContext _context;

    public UpdateBookStatusHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(UpdateBookStatus request, CancellationToken cancellationToken)
    {
        var book = await _context.Books.FirstOrDefaultAsync(
            b => b.Id == request.Id, cancellationToken);

        if (book == null)
        {
            throw new KeyNotFoundException($"Книга с индексом {request.Id} не найдена");
        }
        
        book.BookStatus = request.Dto.BookStatus;
        
        _context.Books.Update(book);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
using LibraryAPI.DbContext;
using LibraryAPI.Models.Entities;
using MediatR;

namespace LibraryAPI.Handlers.Books.Commands.Create;

public sealed record CreateBook(BookCommandDto Dto) : IRequest<int>;

file sealed class CreateBookHandler : IRequest<CreateBook>
{
    private readonly LibraryInfoContext _context;

    public CreateBookHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(CreateBook request, CancellationToken cancellationToken)
    {
        var book = new Book(
            request.Dto.Name,
            request.Dto.AuthorId,
            request.Dto.BookStatus,
            request.Dto.Author);

        await _context.Books.AddAsync(book, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
    
}

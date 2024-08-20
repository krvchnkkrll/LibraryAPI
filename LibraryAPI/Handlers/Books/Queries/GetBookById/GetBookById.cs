using LibraryAPI.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Handlers.Books.Queries.GetBookById;

public sealed record GetBookById(int Id) : IRequest<BooksQueriesDto>;

file sealed class GetBookByIdHandler : IRequestHandler<GetBookById, BooksQueriesDto>
{
    private readonly LibraryInfoContext _context;

    public GetBookByIdHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<BooksQueriesDto> Handle(GetBookById request, CancellationToken cancellationToken)
    {
        
        
        var book = await _context.Books.Include(book => book.Author).FirstOrDefaultAsync(
            b => b.Id == request.Id, cancellationToken);

        if (book == null)
        {
            throw new KeyNotFoundException();
        }

        var bookToReturn = new BooksQueriesDto()
        {
            Id = book.Id,
            Name = book.Name,
            AuthorId = book.AuthorId,
            BookStatus = book.BookStatus,
            Genre = book.Genre,
            Author = book.Author
        };

        return bookToReturn;
    }
}
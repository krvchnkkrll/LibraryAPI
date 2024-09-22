using LibraryAPI.Application.Handlers.Authors.Queries;
using LibraryAPI.Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Application.Handlers.Books.Queries.GetBookById;

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
            BookStatus = book.BookStatus,
            Genre = book.Genre,
            AuthorId = book.AuthorId,
            Author = new AuthorQueriesDto
            {
                Id = book.Author!.Id,
                Surname = book.Author.Surname,
                Name = book.Author.Name,
                Patronymic = book.Author.Patronymic
            },
        };

        return bookToReturn;
    }
}
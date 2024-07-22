using LibraryAPI.DbContext;
using LibraryAPI.Models;
using LibraryAPI.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Handlers.Books.Queries;

public sealed record GetAllBooks(
    string? Name,
    Genre? Genre,
    BookStatus? BookStatus,
    int PageNumber, 
    int PageSize): IRequest<BooksPaginationDto>;

file sealed class GetAllBooksHandler : IRequestHandler<GetAllBooks, BooksPaginationDto>
{
    private readonly LibraryInfoContext _context;

    public GetAllBooksHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<BooksPaginationDto> Handle(GetAllBooks request, CancellationToken cancellationToken)
    {
        var books = _context.Books.AsQueryable();

        if (!string.IsNullOrEmpty(request.Name))
        {
            books = books.Where(b => b.Name.Contains(request.Name));
        }
        
        if (request.Genre != null)
        {
            books = books.Where(b => b.Genre == request.Genre);
        }

        if (request.BookStatus != null)
        {
            books = books.Where(b => b.BookStatus == request.BookStatus);
        }
        
        var totalItemCount = await books.CountAsync(cancellationToken);

        var paginationMetaData = new PaginationMetaData(
            totalItemCount, request.PageSize, request.PageNumber);

        var booksToReturn = await books
            .OrderBy(b => b.Name)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .Select(b => new BooksQueriesDto
            {
                Id = b.Id,
                Name = b.Name,
                Genre = b.Genre,
                Author = b.Author,
                AuthorId = b.AuthorId,
                BookStatus = b.BookStatus
            })
            .ToListAsync(cancellationToken);

        return new BooksPaginationDto(booksToReturn, paginationMetaData);
    }
}
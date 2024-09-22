using LibraryAPI.Core.Models;

namespace LibraryAPI.Application.Handlers.Books.Queries;

public class BooksPaginationDto
{
    public IEnumerable<BooksQueriesDto> Books { get; set; }
    public PaginationMetaData PaginationMetaData { get; set; }

    public BooksPaginationDto(IEnumerable<BooksQueriesDto> books, PaginationMetaData paginationMetaData)
    {
        Books = books;
        PaginationMetaData = paginationMetaData;
    }
}
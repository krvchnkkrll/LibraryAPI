using LibraryAPI.Handlers.Authors.Queries;
using LibraryAPI.Models;

namespace LibraryAPI.Handlers.Books.Queries;

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
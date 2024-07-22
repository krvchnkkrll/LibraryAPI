using LibraryAPI.Handlers.Books.Queries;
using LibraryAPI.Models;

namespace LibraryAPI.Handlers.UserBooks.Queries;

public class UserBooksPaginationDto
{
    public IEnumerable<UserBooksQueriesDto> UserBooks { get; set; }
    
    public PaginationMetaData PaginationMetaData { get; set; }

    public UserBooksPaginationDto(IEnumerable<UserBooksQueriesDto> userbooks, PaginationMetaData paginationMetaData)
    {
        UserBooks = userbooks;
        PaginationMetaData = paginationMetaData;
    }
}


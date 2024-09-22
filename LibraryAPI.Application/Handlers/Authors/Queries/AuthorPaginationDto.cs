using LibraryAPI.Core.Models;

namespace LibraryAPI.Application.Handlers.Authors.Queries;

public class AuthorPaginationDto
{
    public IEnumerable<AuthorQueriesDto> Authors { get; set; }
    public PaginationMetaData PaginationMetaData { get; set; }

    public AuthorPaginationDto(IEnumerable<AuthorQueriesDto> authors, PaginationMetaData paginationMetaData)
    {
        Authors = authors;
        PaginationMetaData = paginationMetaData;
    }
}
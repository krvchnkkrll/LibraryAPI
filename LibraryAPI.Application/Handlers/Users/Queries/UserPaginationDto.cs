
using LibraryAPI.Core.Models;

namespace LibraryAPI.Application.Handlers.Users.Queries;

public class UserPaginationDto
{
    public IEnumerable<UserQueriesDto> Users { get; set; }
    public PaginationMetaData PaginationMetaData { get; set; }

    public UserPaginationDto(IEnumerable<UserQueriesDto> users, PaginationMetaData paginationMetaData)
    {
        Users = users;
        PaginationMetaData = paginationMetaData;
    }
}
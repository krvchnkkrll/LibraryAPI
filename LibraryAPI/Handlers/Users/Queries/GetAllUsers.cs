using LibraryAPI.DbContext;
using LibraryAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Handlers.Users.Queries;

public sealed record GetAllUsers(
    string? Surname, 
    string? Name, 
    string? Patronymic, 
    bool? IsItStaff,
    string? SearchQuery, 
    int PageNumber, 
    int PageSize) : IRequest<UserPaginationDto>;

file sealed class GetAllUsersHandler : IRequestHandler<GetAllUsers, UserPaginationDto>
{
    private readonly LibraryInfoContext _context;

    public GetAllUsersHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UserPaginationDto> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        var users = _context.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Surname))
        {
            var surname = request.Surname.Trim();
            users = users.Where(u => u.Surname == surname);
        }
        
        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            var name = request.Name.Trim();
            users = users.Where(u => u.Name == name);
        }
        
        if (!string.IsNullOrWhiteSpace(request.Patronymic))
        {
            var patronymic = request.Patronymic.Trim();
            users = users.Where(u => u.Patronymic == patronymic);
        }

        if (request.IsItStaff.HasValue)
        {
            users = users.Where(u => u.IsItStaff == request.IsItStaff.Value);
        }
        
        if (!string.IsNullOrWhiteSpace(request.SearchQuery))
        {
            var searchQuery = request.SearchQuery.Trim();
            users = users.Where(u => 
                u.Name.Contains(searchQuery) ||
                u.Surname.Contains(searchQuery) ||
                (u.Patronymic != null && u.Patronymic.Contains(searchQuery)));
        }
        
        var totalItemCount = await users.CountAsync(cancellationToken);

        var paginationMetaData = new PaginationMetaData(
            totalItemCount, request.PageSize, request.PageNumber);

        var authorsToReturn = await users
            .OrderBy(u => u.Surname)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .Select(u => new UserQueriesDto
            {
                Id = u.Id,
                Name = u.Name, 
                Surname = u.Surname,
                Patronymic = u.Patronymic,
                IsItStaff = u.IsItStaff
            })
            .ToListAsync(cancellationToken);

        return new UserPaginationDto(authorsToReturn, paginationMetaData);
    }
}
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Application.Handlers.Authors.Queries.GetAllAuthors;

public sealed record GetAllAuthors(
    string? Surname, 
    string? Name, 
    string? Patronymic, 
    string? SearchQuery, 
    int PageNumber, 
    int PageSize) : IRequest<AuthorPaginationDto>;

file sealed class GetAllAuthorsHandler : IRequestHandler<GetAllAuthors, AuthorPaginationDto>
{
    private readonly LibraryInfoContext _context;

    public GetAllAuthorsHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<AuthorPaginationDto> Handle(GetAllAuthors request, CancellationToken cancellationToken)
    {
        var collection = _context.Authors.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            var name = request.Name.Trim();
            collection = collection.Where(a => a.Name == name);
        }

        if (!string.IsNullOrWhiteSpace(request.Surname))
        {
            var surname = request.Surname.Trim();
            collection = collection.Where(a => a.Surname == surname);
        }

        if (!string.IsNullOrWhiteSpace(request.Patronymic))
        {
            var patronymic = request.Patronymic.Trim();
            collection = collection.Where(a => a.Patronymic == patronymic);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchQuery))
        {
            var searchQuery = request.SearchQuery.Trim();
            collection = collection.Where(a => 
                a.Name.Contains(searchQuery) ||
                a.Surname.Contains(searchQuery) ||
                (a.Patronymic != null && a.Patronymic.Contains(searchQuery)));
        }

        var totalItemCount = await collection.CountAsync(cancellationToken);

        var paginationMetaData = new PaginationMetaData(
            totalItemCount, request.PageSize, request.PageNumber);

        var authorsToReturn = await collection
            .OrderBy(a => a.Surname)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .Select(a => new AuthorQueriesDto
            {
                Id = a.Id,
                Name = a.Name, 
                Surname = a.Surname,
                Patronymic = a.Patronymic
            })
            .ToListAsync(cancellationToken);

        return new AuthorPaginationDto(authorsToReturn, paginationMetaData);
    }
}
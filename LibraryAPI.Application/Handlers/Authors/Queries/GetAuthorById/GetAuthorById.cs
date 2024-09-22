using LibraryAPI.Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Application.Handlers.Authors.Queries.GetAuthorById;

public sealed record GetAuthorById(int Id) : IRequest<AuthorQueriesDto>;

file sealed class GetAuthorByIdHandler : IRequestHandler<GetAuthorById, AuthorQueriesDto>
{
    private readonly LibraryInfoContext _context;

    public GetAuthorByIdHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<AuthorQueriesDto> Handle(GetAuthorById request, CancellationToken cancellationToken)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(
            a => a.Id == request.Id, cancellationToken);

        if (author == null)
        {
            throw new KeyNotFoundException($"Автор с индексом {request.Id} не найден.");
        }

        var authorToReturn = new AuthorQueriesDto()
        {
            Id = author.Id,
            Surname = author.Surname,
            Name = author.Name,
            Patronymic = author.Patronymic
        };

        return authorToReturn;
    }
}

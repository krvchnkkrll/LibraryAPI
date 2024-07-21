using LibraryAPI.DbContext;
using LibraryAPI.Models.Entities;
using MediatR;

namespace LibraryAPI.Handlers.Authors.Commands.Create;

public record CreateAuthor(AuthorCommandDto Dto) : IRequest;

file sealed class CreateAuthorHandler : IRequestHandler<CreateAuthor>
{
    private readonly LibraryInfoContext _context;

    public CreateAuthorHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(CreateAuthor request, CancellationToken cancellationToken)
    {
        var author = new Author
        (
            request.Dto.Surname,
            request.Dto.Name,
            request.Dto.Patronymic
        );

        await _context.Authors.AddAsync(author, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
using LibraryAPI.DbContext;
using LibraryAPI.Models.Entities;
using MediatR;

namespace LibraryAPI.Handlers.UserBooks.Commands.Create;

public sealed record CreateUserBooks(UserBookCommandDto Dto) : IRequest;

file sealed class CreateUserBooksHandler : IRequest<CreateUserBooks>
{
    private readonly LibraryInfoContext _context;

    public CreateUserBooksHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(CreateUserBooks request, CancellationToken cancellationToken)
    {
        var userBook = new UserBook(
            request.Dto.UserId,
            request.Dto.BookId,
            request.Dto.DateReturn);

        await _context.UserBooks.AddAsync(userBook, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
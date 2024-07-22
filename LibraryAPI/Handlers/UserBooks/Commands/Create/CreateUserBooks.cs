using LibraryAPI.DbContext;
using LibraryAPI.Models.Entities;
using MediatR;

namespace LibraryAPI.Handlers.UserBooks.Commands.Create;

public sealed record CreateUserBooks(UserBooksCommandDto Dto) : IRequest;

file sealed class CreateUserBooksHandler : IRequest<CreateUserBooks>
{
    private readonly LibraryInfoContext _context;

    public CreateUserBooksHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(CreateUserBooks request, CancellationToken cancellationToken)
    {
        var userbook = new UserBook(
            request.Dto.UserId,
            request.Dto.BookId,
            request.Dto.DateReturn,
            request.Dto.User,
            request.Dto.Book);

        await _context.UserBooks.AddAsync(userbook, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
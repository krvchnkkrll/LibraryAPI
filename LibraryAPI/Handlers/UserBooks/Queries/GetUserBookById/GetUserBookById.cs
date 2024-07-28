using LibraryAPI.DbContext;
using LibraryAPI.Handlers.Books.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Handlers.UserBooks.Queries.GetUserBookById;

public sealed record GetUserBookById(int Id) : IRequest<UserBooksQueriesDto>;

file sealed class GetUserBookByIdHandler : IRequestHandler<GetUserBookById, UserBooksQueriesDto>
{
    private readonly LibraryInfoContext _context;

    public GetUserBookByIdHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UserBooksQueriesDto> Handle(GetUserBookById request, CancellationToken cancellationToken)
    {
        var userBook = await _context.UserBooks.
            Include(userBook => userBook.Book).
            Include(userBook => userBook.User).
            FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (userBook == null)
        {
            throw new KeyNotFoundException($"Книга пользователя с индексом {request.Id} не найдена");
        }

        var userBookToReturn = new UserBooksQueriesDto()
        {
            Id = userBook.Id,
            BookId = userBook.BookId,
            Book = userBook.Book,
            UserId = userBook.UserId,
            User = userBook.User,
            DateReturn = userBook.DateReturn,
            DateReceipt = userBook.DateReceipt
        };

        return userBookToReturn;
    }
}
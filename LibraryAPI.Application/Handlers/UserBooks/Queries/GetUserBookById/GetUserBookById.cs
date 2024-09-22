using LibraryAPI.Application.Handlers.Books.Queries;
using LibraryAPI.Application.Handlers.Users.Queries;
using LibraryAPI.Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Application.Handlers.UserBooks.Queries.GetUserBookById;

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
            UserId = userBook.UserId,
            User = new UserQueriesDto
            {
                Surname = userBook.User!.Surname,
                Name = userBook.User.Name,
                Patronymic = userBook.User.Patronymic
            },
            BookId = userBook.BookId,
            Book = new BooksQueriesDto
            {
                Name = userBook.Book!.Name,
                Genre = userBook.Book.Genre,
                BookStatus = userBook.Book.BookStatus,
            },
            DateReturn = userBook.DateReturn,
            DateReceipt = userBook.DateReceipt,
            BorrowPeriod = userBook.BorrowPeriod
        };

        return userBookToReturn;
    }
}
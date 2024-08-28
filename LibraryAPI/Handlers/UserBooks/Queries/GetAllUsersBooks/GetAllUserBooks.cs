using LibraryAPI.DbContext;
using LibraryAPI.Handlers.Books.Queries;
using LibraryAPI.Handlers.Users.Queries;
using LibraryAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Handlers.UserBooks.Queries.GetAllUsersBooks;

public sealed record GetAllUserBooks(
    int PageNumber, 
    int PageSize): IRequest<UserBooksPaginationDto>;

file sealed class GetAllUserBooksHandler : IRequestHandler<GetAllUserBooks, UserBooksPaginationDto>
{
    private readonly LibraryInfoContext _context;

    public GetAllUserBooksHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UserBooksPaginationDto> Handle(GetAllUserBooks request, CancellationToken cancellationToken)
    {
        var userBooks = _context.UserBooks.AsQueryable();

        var totalItemCount = await userBooks.CountAsync(cancellationToken);
        
        var paginationMetaData = new PaginationMetaData(
            totalItemCount, request.PageSize, request.PageNumber);

        var userBooksToReturn = await userBooks
            .OrderBy(u => u.DateReceipt)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .Select(u => new UserBooksQueriesDto()
            {
                Id = u.Id,
                UserId = u.UserId,
                User = new UserQueriesDto
                {
                    Surname = u.User!.Surname,
                    Name = u.User.Name,
                    Patronymic = u.User.Patronymic,
                    Password = u.User.Password
                },
                BookId = u.BookId,
                Book = new BooksQueriesDto
                {
                    Name = u.Book!.Name,
                    Genre = u.Book.Genre,
                    BookStatus = u.Book.BookStatus,
                },
                DateReceipt = u.DateReceipt,
                DateReturn = u.DateReturn,
                BorrowPeriod = u.BorrowPeriod,
            })
            .ToListAsync(cancellationToken);

        return new UserBooksPaginationDto(userBooksToReturn, paginationMetaData);
    }
}
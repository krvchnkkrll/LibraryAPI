using LibraryAPI.DbContext;

using MediatR;
using Microsoft.EntityFrameworkCore;


namespace LibraryAPI.Handlers.UserBooks.Queries.GetAllUsersBooks;

public sealed record GetAllUsersBooksWithoutPagination() : IRequest<IEnumerable<UserBooksQueriesDto>>;
    
file sealed class GetAllUsersBooksWithoutPaginationHandler : IRequestHandler<GetAllUsersBooksWithoutPagination, IEnumerable<UserBooksQueriesDto>>
{
    private readonly LibraryInfoContext _context;

    public GetAllUsersBooksWithoutPaginationHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<UserBooksQueriesDto>> Handle(GetAllUsersBooksWithoutPagination request,
        CancellationToken cancellationToken)
    {
        var userBooks = await _context.UserBooks
            .Select(u => new UserBooksQueriesDto
            {
                Id = u.Id,
                UserId = u.UserId,
                BookId = u.BookId,
                DateReceipt = u.DateReceipt,
                DateReturn = u.DateReturn,
                BorrowPeriod = u.BorrowPeriod,
            })
            .ToListAsync(cancellationToken);

        return userBooks;
    }
}

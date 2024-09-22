using LibraryAPI.Application.Handlers.UserBooks.Queries.GetAllUsersBooks;
using MediatR;

namespace LibraryAPI.Application.Services.Hangfire;

public class UserBookService
{
    private readonly IMediator _mediator;
    private readonly BookService _bookService;

    public UserBookService(IMediator mediator, BookService bookService)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
    }

    public async Task ChangeBookStatus(CancellationToken cancellationToken)
    {
        var requestToIssuedUsersBooks = new GetAllUsersBooksWithoutPagination();
        var issuedUserBooks = await _mediator.Send(requestToIssuedUsersBooks, cancellationToken);

        foreach (var userBook in issuedUserBooks)
        {
            if (userBook.DateReturn == null &&
                (DateTime.UtcNow - userBook.DateReceipt).TotalDays > userBook.BorrowPeriod)
            {
                await _bookService.UpdateBookStatusToOverdue(userBook.BookId, cancellationToken);
            }
        }
    }
}
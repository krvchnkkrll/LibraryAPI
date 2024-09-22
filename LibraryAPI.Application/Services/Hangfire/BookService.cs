using LibraryAPI.Application.Handlers.Books.Commands;
using LibraryAPI.Application.Handlers.Books.Commands.Update;
using LibraryAPI.Core.Models.Enums;
using MediatR;

namespace LibraryAPI.Application.Services.Hangfire;

public class BookService
{
    private readonly IMediator _mediator;
    
    public BookService(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    public async Task UpdateBookStatusToOverdue(int id, CancellationToken cancellationToken)
    {
        var bookStatus = new BookStatusCommandDto()
        {
            BookStatus = BookStatus.Overdue
        };

        await _mediator.Send(new UpdateBookStatus(id, bookStatus), cancellationToken);
    }
}
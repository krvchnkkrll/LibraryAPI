using LibraryAPI.Handlers.Books.Commands;
using LibraryAPI.Handlers.Books.Commands.Update;
using LibraryAPI.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Services.Hangfire;

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
using LibraryAPI.Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Application.Handlers.UserBooks.Commands.Update;

public record UpdateUserBook(int Id, UserBookCommandDto Dto) : IRequest;

file sealed class UpdateUserBookHandler : IRequestHandler<UpdateUserBook>
{
    private readonly LibraryInfoContext _context;

    public UpdateUserBookHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(UpdateUserBook request, CancellationToken cancellationToken)
    {
        var userBook = await _context.UserBooks.FirstOrDefaultAsync(
            u => u.Id == request.Id, cancellationToken);

        if (userBook == null)
        {
            throw new KeyNotFoundException($"Запись о книги пользователя с индексом {request.Id} не найдена");
        }
        
        userBook.BookId = request.Dto.BookId;
        userBook.DateReceipt = request.Dto.DateReceipt.ToUniversalTime();
        userBook.DateReturn = request.Dto.DateReturn?.ToUniversalTime();
        userBook.BorrowPeriod = request.Dto.BorrowPeriod;

        _context.UserBooks.Update(userBook);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
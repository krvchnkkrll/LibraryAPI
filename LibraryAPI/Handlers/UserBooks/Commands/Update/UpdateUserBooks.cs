using LibraryAPI.DbContext;
using LibraryAPI.Handlers.Books.Commands.Update;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Handlers.UserBooks.Commands.Update;

public record UpdateUserBook(int Id, UserBooksCommandDto Dto) : IRequest;

file sealed class UpdateUserBookHandler : IRequest<UpdateBook>
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

        userBook.UserId = request.Dto.UserId;
        userBook.BookId = request.Dto.BookId;
        userBook.DateReceipt = request.Dto.DateReceipt;
        userBook.DateReturn = request.Dto.DateReturn;
        userBook.Book = request.Dto.Book;
        userBook.User = request.Dto.User;

        _context.UserBooks.Update(userBook);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
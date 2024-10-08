﻿using LibraryAPI.Core.Models.Entities;
using LibraryAPI.Infrastructure.DbContext;
using MediatR;

namespace LibraryAPI.Application.Handlers.UserBooks.Commands.Create;

public sealed record CreateUserBooks(UserBookCommandDto Dto) : IRequest;

file sealed class CreateUserBooksHandler : IRequestHandler<CreateUserBooks>
{
    private readonly LibraryInfoContext _context;

    public CreateUserBooksHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(CreateUserBooks request, CancellationToken cancellationToken)
    {
        var userBook = new UserBook(
            request.Dto.UserId,
            request.Dto.BookId,
            request.Dto.DateReturn,
            request.Dto.BorrowPeriod);

        await _context.UserBooks.AddAsync(userBook, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
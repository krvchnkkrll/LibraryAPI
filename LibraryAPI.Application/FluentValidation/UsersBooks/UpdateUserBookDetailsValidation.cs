﻿using FluentValidation;
using LibraryAPI.Application.Handlers.UserBooks.Commands.Update;

namespace LibraryAPI.Application.FluentValidation.UsersBooks;

public class UpdateUserBookDetailsValidation : AbstractValidator<UpdateUserBook>
{
    public UpdateUserBookDetailsValidation()
    {
        RuleFor(x => x.Dto.BookId)
            .NotEmpty().WithMessage("Идентификатор книги должен быть заполнен");
        
        RuleFor(x=>x.Dto.UserId)
            .NotEmpty().WithMessage("Идентификатор пользователя должен быть заполнен");
    }
}
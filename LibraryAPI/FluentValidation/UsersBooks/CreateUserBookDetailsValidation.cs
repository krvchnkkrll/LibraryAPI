using FluentValidation;
using LibraryAPI.Handlers.UserBooks.Commands.Create;

namespace LibraryAPI.FluentValidation.UsersBooks;

/// <summary>
/// DateReceipt создается автоматически текущей датой 
/// DateReturn заполняется при возврате книги
/// </summary>
/// 
public class CreateUserBookDetailsValidation : AbstractValidator<CreateUserBooks>
{
    public CreateUserBookDetailsValidation()
    {
        RuleFor(x => x.Dto.BookId)
            .NotEmpty().WithMessage("Идентификатор книги должен быть заполнен");
        
        RuleFor(x=>x.Dto.UserId)
            .NotEmpty().WithMessage("Идентификатор пользователя должен быть заполнен");
    }
}
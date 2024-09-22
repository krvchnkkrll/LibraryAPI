using FluentValidation;
using LibraryAPI.Application.Handlers.Books.Commands.Update;

namespace LibraryAPI.Application.FluentValidation.Books;

public class UpdateBookDetailsValidation : AbstractValidator<UpdateBook>
{
    public UpdateBookDetailsValidation()
    {
        RuleFor(x => x.Dto.Name)
            .NotEmpty().WithMessage("Название книги должно быть указано")
            .MaximumLength(30).WithMessage("Максимальное количесво символов - 30");

        RuleFor(x => x.Dto.AuthorId)
            .NotEmpty().WithMessage("Автор должен быть указан");
    }
}
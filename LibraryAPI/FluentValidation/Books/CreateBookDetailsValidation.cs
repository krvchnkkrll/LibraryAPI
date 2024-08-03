using FluentValidation;
using LibraryAPI.Handlers.Books.Commands.Create;

namespace LibraryAPI.FluentValidation.Books;

public class CreateBookDetailsValidation : AbstractValidator<CreateBook>
{
    public CreateBookDetailsValidation()
    {
        RuleFor(x => x.Dto.Name)
            .NotEmpty().WithMessage("Название книги должно быть указано")
            .MaximumLength(30).WithMessage("Максимальное количесво символов - 30");

        RuleFor(x => x.Dto.AuthorId)
            .NotEmpty().WithMessage("Автор должен быть указан");
    }
}
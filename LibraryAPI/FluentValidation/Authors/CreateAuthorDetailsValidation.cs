using FluentValidation;
using LibraryAPI.Handlers.Authors.Commands;
using LibraryAPI.Handlers.Authors.Commands.Create;

namespace LibraryAPI.FluentValidation.Authors;

public class CreateAuthorDetailsValidation : AbstractValidator<CreateAuthor>
{
    public CreateAuthorDetailsValidation()
    {
        RuleFor(x => x.Dto.Surname)
            .NotEmpty().WithMessage("Фамилия должна быть указана")
            .MinimumLength(3).WithMessage("Минимальное количество символов - 3")
            .MaximumLength(30).WithMessage("Максимальное количесво символов - 30");
        
        RuleFor(x => x.Dto.Name)
            .NotEmpty().WithMessage("Имя должно быть указано")
            .MinimumLength(3).WithMessage("Минимальное количество символов - 3")
            .MaximumLength(30).WithMessage("Максимальное количесво символов - 30");
        
        RuleFor(x => x.Dto.Patronymic)
            .NotEmpty().WithMessage("Отчество должно быть указано")
            .MinimumLength(3).WithMessage("Минимальное количество символов - 3")
            .MaximumLength(30).WithMessage("Максимальное количесво символов - 30");
    }
}
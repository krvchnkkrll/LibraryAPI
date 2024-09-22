using FluentValidation;
using LibraryAPI.Application.Handlers.Users.Commands.Create;

namespace LibraryAPI.Application.FluentValidation.Users;

public class CreateUserDetailsValidation : AbstractValidator<CreateUser>
{
    public CreateUserDetailsValidation()
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
            .MinimumLength(3).WithMessage("Минимальное количество символов - 3")
            .MaximumLength(30).WithMessage("Максимальное количесво символов - 30");
        
        RuleFor(x=> x.Dto.Login)
            .NotEmpty().WithMessage("Логин должен быть указан")
            .MinimumLength(3).WithMessage("Минимальное количество символов - 3")
            .MaximumLength(30).WithMessage("Максимальное количесво символов - 30");

        RuleFor(x => x.Dto.Password)
            .NotEmpty().WithMessage("Пароль должен быть указан")
            .MinimumLength(8).WithMessage("Минимальное количество символов - 8")
            .Matches(@"[A-Z]").WithMessage("Пароль должен содержать заглавную букву")
            .Matches(@"[a-z]").WithMessage("Пароль должен содержать одну строчную букву")
            .Matches(@"[\d]").WithMessage("Пароль должен содержать одну цифру")
            .Matches(@"[^\da-zA-z]").WithMessage("Пароль должен содержать один специальный символ");
    }
}
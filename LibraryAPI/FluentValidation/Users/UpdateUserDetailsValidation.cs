using FluentValidation;
using LibraryAPI.Handlers.Users.Commands.Update;

namespace LibraryAPI.FluentValidation.Users;

public class UpdateUserDetailsValidation : AbstractValidator<UpdateUser>
{
    public UpdateUserDetailsValidation()
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
            .MinimumLength(8).WithMessage("Минимальное количество символов - 3")
            .Matches(@"[A-Z]").WithMessage("Пароль должен содержать заглавную букву")
            .Matches(@"[a-z]").WithMessage("Пароль должен содержать одну строчную букву")
            .Matches(@"х[\d]").WithMessage("Пароль должен содержать одну цифру")
            .Matches(@"[^\da-zA-z]").WithMessage("Пароль должен содержать один специальный символ");
    }
}
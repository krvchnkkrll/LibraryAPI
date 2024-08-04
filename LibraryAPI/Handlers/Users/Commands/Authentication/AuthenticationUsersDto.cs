namespace LibraryAPI.Handlers.Users.Commands.Authentication;

public class AuthenticationUsersDto
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public required string Login { get; set; }

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public required string Password { get; set; }
}
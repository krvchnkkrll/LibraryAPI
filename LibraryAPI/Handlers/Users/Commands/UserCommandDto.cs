namespace LibraryAPI.Handlers.Users.Commands;

public class UserCommandDto
{
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public required string Surname { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Отчество пользователя, необязательное
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Логин пользователя
    /// </summary>
    public required string Login { get; set; }

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Является ли пользователь сотрудником
    /// </summary>
    public bool IsItStaff { get; set; } = false;
}
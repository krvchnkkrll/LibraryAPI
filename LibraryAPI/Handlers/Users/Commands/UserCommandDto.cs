namespace LibraryAPI.Handlers.Users.Commands;

public class UserCommandDto
{
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Отчество пользователя, необязательное
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Является ли пользователь сотрудником
    /// </summary>
    public bool IsItStaff { get; set; } = false;
}
using System.Text.Json.Serialization;

namespace LibraryAPI.Handlers.Users.Queries;

public class UserQueriesDto
{
    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Id { get; set; }

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string Surname { get; set; } = default!;

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Отчество пользователя, необязательное
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Логин пользователя
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Login { get; set; } = default!;

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Password { get; set; } = default!;

    /// <summary>
    /// Является ли пользователь сотрудником
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool IsItStaff { get; set; } = false;
}
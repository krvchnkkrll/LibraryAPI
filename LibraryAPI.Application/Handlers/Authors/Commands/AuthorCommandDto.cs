namespace LibraryAPI.Application.Handlers.Authors.Commands;

public class AuthorCommandDto
{
    /// <summary>
    /// Фамилия автора
    /// </summary>
    public string Surname { get; set; } = string.Empty;

    /// <summary>
    /// Имя автора
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Отчество автора
    /// </summary>
    public string? Patronymic { get; set; }
}
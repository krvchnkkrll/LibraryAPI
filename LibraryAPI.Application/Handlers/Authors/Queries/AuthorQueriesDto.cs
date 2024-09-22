using System.Text.Json.Serialization;

namespace LibraryAPI.Application.Handlers.Authors.Queries;

public class AuthorQueriesDto
{
    /// <summary>
    /// Уникальный идентификатор автора
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Id { get; set; }

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
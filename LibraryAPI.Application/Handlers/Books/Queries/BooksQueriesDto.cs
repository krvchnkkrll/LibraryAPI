using System.Text.Json.Serialization;
using LibraryAPI.Application.Handlers.Authors.Queries;
using LibraryAPI.Core.Models.Enums;

namespace LibraryAPI.Application.Handlers.Books.Queries;

public class BooksQueriesDto
{
    /// <summary>
    /// Уникальный идентификатор книги
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Id { get; set; }

    /// <summary>
    /// Название книги
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    ///  Жанр книги из перечисления жанров
    /// </summary>
    public Genre Genre { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор автора
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int AuthorId { get; set; }
    
    /// <summary>
    /// Автор
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public AuthorQueriesDto? Author { get; set; }
    
    /// <summary>
    /// Статус книги
    /// </summary>
    public BookStatus BookStatus { get; set; }
}
using System.Text.Json.Serialization;
using LibraryAPI.Application.Handlers.Authors.Queries;
using LibraryAPI.Core.Models.Enums;

namespace LibraryAPI.Application.Handlers.Books.Queries;

public class BooksQueriesSqlDto
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
    /// Жанр книги из перечисления жанров
    /// </summary>
    public Genre Genre { get; set; }
    
    public string GenreName => Genre.ToString(); 
    
    /// <summary>
    /// Уникальный идентификатор автора
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int AuthorId { get; set; }
    
    /// <summary>
    /// Фамилия автора
    /// </summary>
    public string AuthorSurname { get; set; } = default!;

    /// <summary>
    /// Имя автора
    /// </summary>
    public string AuthorName { get; set; } = default!;

    /// <summary>
    /// Отчество автора
    /// </summary>
    public string AuthorPatronymic { get; set; } = default!;
    
    /// <summary>
    /// Статус книги
    /// </summary>
    public BookStatus BookStatus { get; set; }

    public string BookStatusName => BookStatus.ToString();
}
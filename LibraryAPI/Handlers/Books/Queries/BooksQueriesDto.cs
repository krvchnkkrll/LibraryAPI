using LibraryAPI.Models.Entities;
using LibraryAPI.Models.Enums;

namespace LibraryAPI.Handlers.Books.Queries;

public class BooksQueriesDto
{
    /// <summary>
    /// Уникальный идентификатор книги
    /// </summary>
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
    /// Автор
    /// </summary>
    public Author? Author { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор автора
    /// </summary>
    public int AuthorId { get; set; }
    
    /// <summary>
    /// Статус книги
    /// </summary>
    public BookStatus BookStatus { get; set; }
}
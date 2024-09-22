using LibraryAPI.Core.Models.Enums;

namespace LibraryAPI.Application.Handlers.Books.Commands;

public class BookCommandDto
{
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
    public int AuthorId { get; set; }
    
    /// <summary>
    /// Статус книги
    /// </summary>
    public BookStatus BookStatus { get; set; }
}
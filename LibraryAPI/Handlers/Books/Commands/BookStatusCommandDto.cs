using LibraryAPI.Models.Enums;

namespace LibraryAPI.Handlers.Books.Commands;

public class BookStatusCommandDto
{
    /// <summary>
    /// Статус книги
    /// </summary>
    public BookStatus BookStatus { get; set; }
}
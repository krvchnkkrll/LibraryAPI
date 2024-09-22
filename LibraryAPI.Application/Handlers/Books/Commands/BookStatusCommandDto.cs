using LibraryAPI.Core.Models.Enums;

namespace LibraryAPI.Application.Handlers.Books.Commands;

public class BookStatusCommandDto
{
    /// <summary>
    /// Статус книги
    /// </summary>
    public BookStatus BookStatus { get; set; }
}
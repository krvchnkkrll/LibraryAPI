using LibraryAPI.Models.Entities;

namespace LibraryAPI.Handlers.UserBooks.Commands;

public class UserBookCommandDto
{
    /// <summary>
    /// Уникальный идентификатор пользователя получившего книгу
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Идентификатор книги
    /// </summary>
    public int BookId { get; set; }
    
    /// <summary>
    /// Дата получения книги
    /// </summary>
    public DateTime DateReceipt { get; set; }
    
    /// <summary>
    /// Дата возврата книги
    /// </summary>
    public DateTime? DateReturn { get; set; }
}
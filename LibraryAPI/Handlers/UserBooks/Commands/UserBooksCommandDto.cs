using LibraryAPI.Models.Entities;

namespace LibraryAPI.Handlers.UserBooks.Commands;

public class UserBooksCommandDto
{
    /// <summary>
    /// Идентификатор записи выдачи книги пользователю
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Пользователь получивший книгу
    /// </summary>
    public User? User { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор пользователя получившего книгу
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Книга, которую забрал пользователь
    /// </summary>
    public Book? Book { get; set; }
    
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
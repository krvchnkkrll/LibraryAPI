using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryAPI.Models.Enums;

namespace LibraryAPI.Models.Entities;

public class UserBook
{
    public UserBook(int userId, int bookId,  DateTime? dateReturn, int borrowPeriod = 14)
    {
        UserId = userId;
        BookId = bookId;
        DateReceipt = DateTime.UtcNow;
        DateReturn = dateReturn;
        BorrowPeriod = borrowPeriod;
    }

    /// <summary>
    /// Идентификатор записи выдачи книги пользователю
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    
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
    [Required]
    public DateTime DateReceipt { get; set; }

    /// <summary>
    /// Срок выдачи
    /// </summary>
    public int BorrowPeriod { get; set; }
    
    /// <summary>
    /// Дата возврата книги
    /// </summary>
    public DateTime? DateReturn { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryAPI.Models.Enums;

namespace LibraryAPI.Models.Entities;

public class Book
{
    private Book(){}
    
    public Book(string name, int authorId, BookStatus bookStatus)
    {
        Name = name;
        AuthorId = authorId;
        BookStatus = bookStatus;
    }
    

    /// <summary>
    /// Уникальный идентификатор книги
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    /// <summary>
    /// Название книги
    /// </summary>
    [Required] [MaxLength(50)] 
    public string Name { get; set; } = default!;
    
    /// <summary>
    ///  Жанр книги из перечисления жанров
    /// </summary>
    [Required]
    public Genre Genre { get; set; }
    
    /// <summary>
    /// Автор
    /// </summary>
    [ForeignKey("AuthorId")]
    public Author? Author { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор автора
    /// </summary>
    public int AuthorId { get; set; }
    
    /// <summary>
    /// Статус книги
    /// </summary>
    [Required]
    public BookStatus BookStatus { get; set; }
    
    /// <summary>
    /// Коллекция записей о выдачи книг пользователям
    /// </summary>
    public ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
}
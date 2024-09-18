using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Models.Entities;

public class Author
{
    public Author(string surname, string name, string? patronymic)
    {
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
    }

    /// <summary>
    /// Уникальный идентификатор автора
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    /// <summary>
    /// Фамилия автора
    /// </summary>
    [Required] [MaxLength(30)] public string Surname { get; set; }

    /// <summary>
    /// Имя автора
    /// </summary>
    [Required] [MaxLength(30)] public string Name { get; set; }

    /// <summary>
    /// Отчество автора
    /// </summary>
    [Required] [MaxLength(30)] public string? Patronymic { get; set; }

    /// <summary>
    /// Коллекция записей книг
    /// </summary>
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
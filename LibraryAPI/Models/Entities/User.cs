using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Models.Entities;

public class User
{
    private User(){}
    
    public User(string surname, string name, string? patronymic, string login, string password, bool isItStaff)
    {
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        Login = login;
        Password = password;
        IsItStaff = isItStaff;
    }

    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    [Required] [MaxLength(30)] public string Surname { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required] [MaxLength(30)] public string Name { get; set; }

    /// <summary>
    /// Отчество пользователя, необязательное
    /// </summary>
    [Required] [MaxLength(30)] public string? Patronymic { get; set; }

    /// <summary>
    /// Логин пользователя
    /// </summary>
    [Required] [MaxLength(30)] public string Login { get; set; }

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    [Required] [MaxLength(30)] public string Password { get; set; }

    /// <summary>
    /// Является ли пользователь сотрудником
    /// </summary>
    [Required] public bool IsItStaff { get; set; } = false;
    
    /// <summary>
    /// Коллекция записей о выдачи книг пользователям
    /// </summary>
    public ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
}

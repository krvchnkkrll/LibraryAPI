using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Core.Models.Enums;

public enum Genre
{
    [Display(Name = "Научная фантастика")] ScienceFiction,

    [Display(Name = "Фэнтези")] Fantasy,

    [Display(Name = "Биография")] Biography,

    [Display(Name = "История")] History,

    [Display(Name = "Романтика")] Romance
}
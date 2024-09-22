using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Core.Models.Enums;

public enum BookStatus
{
    [Display(Name = "Доступна")] Аvailable,
    
    [Display(Name = "Выдана")] Issued,

    [Display(Name = "Утеряна")] Lost,
    
    [Display(Name = "Просрочена")] Overdue
 }

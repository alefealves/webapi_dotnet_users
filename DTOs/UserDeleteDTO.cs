using System.ComponentModel.DataAnnotations;

namespace UsersManager.Models;

public class UserDeleteDTO
{
    [Required(ErrorMessage = "O campo IdUserAlter é obrigatório")]
    public int IdUserAlter { get; set; }
    [Required(ErrorMessage = "O campo IdUserDelete é obrigatório")]
    public int IdUserDelete { get; set; }
}
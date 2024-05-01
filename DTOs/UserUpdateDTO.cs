using System.ComponentModel.DataAnnotations;

namespace UsersManager.Models;

public class UserUpdateDTO
{
    [Required(ErrorMessage = "O campo IdUserAlter é obrigatório")]
    public int IdUserAlter { get; set; }
    [Required(ErrorMessage = "O campo IdUser é obrigatório")]
    public int IdUser { get; set; }
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    public string? NivelAcesso { get; set; }
}
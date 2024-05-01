using System.ComponentModel.DataAnnotations;
using UsersManager.Enums;

namespace UsersManager.Models;

public class UserCreateDTO
{
    [Required(ErrorMessage = "O campo IdUserAlter é obrigatório")]
    public int IdUserAlter { get; set; }
    
    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    public string Nome { get; set; }
  
    [Required(ErrorMessage = "O campo Sobrenome é obrigatório")]
    public string Sobrenome { get; set; }
   
    [Required(ErrorMessage = "O campo email é obrigatório"), EmailAddress(ErrorMessage = "Email inválido!")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O campo senha é obrigatório")]
    public string Senha { get; set; }
    
    [Compare("Senha", ErrorMessage = "Senhas não coincidem")]
    public string ConfirmarSenha { get; set; }
    
    [Required(ErrorMessage = "O campo NivelAcesso é obrigatório")]
    public Acesso NivelAcesso { get; set; }
}
using UsersManager.Enums;

namespace UsersManager.Models;

public class UserDTO
{
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    public string? NivelAcesso { get; set; }

}
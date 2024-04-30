using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UsersManager.Enums;

namespace UsersManager.Models;

[Table("user")] 
public class UserModel
{
    
    [Key]
    [Column("id")] 
    public int Id { get; set; }
    
    [Column("nome")]
    public string? Nome { get; set; }
    
    [Column("sobrenome")]
    public string? Sobrenome { get; set; }
    
    [Column("email")] 
    public string? Email { get; set; }
    
    [Column("senha")] 
    public string? Senha { get; set; }
    
    [Column("nivel_acesso")] 
    public Acesso NivelAcesso { get; set; }

    public UserModel(string nome, string sobrenome, string email, string senha, Acesso nivelAcesso)
    {
        Nome = nome;
        Sobrenome = sobrenome;
        Email = email;
        Senha = senha;
        NivelAcesso = nivelAcesso;
    }
}
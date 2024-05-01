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
    public string Nome { get; set; }
    
    [Column("sobrenome")]
    public string Sobrenome { get; set; }
    
    [Column("email")] 
    public string Email { get; set; }
    
    [Column("senha_hash")] 
    public byte[] SenhaHash { get; set; }
    
    [Column("senha_salt")] 
    public byte[] SenhaSalt { get; set; }
    
    [Column("nivel_acesso")] 
    public Acesso NivelAcesso { get; set; }

    [Column("ativo")] public bool Ativo { get; set; } = true;

    [Column("data_create")] public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    
    [Column("id_user_update")] 
    public int IdUserUpdate { get; set; }
    [Column("data_update")] public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
}
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UsersManager.Enums;
using UsersManager.Models;

namespace UsersManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options)
    {            
    }

    public DbSet<UserModel> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        byte[] senhahash;
        byte[] senhasalt; 
        using (var hmac = new HMACSHA512())
        {
            senhasalt = hmac.Key;
            senhahash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("admin"));
        }
        
        modelBuilder.Entity<UserModel>().HasData(
            new
            {
                Id = 1,
                Nome = "admin", 
                Sobrenome = "admin", 
                Email = "admin@admin.com", 
                SenhaHash = senhahash, 
                SenhaSalt = senhasalt,
                NivelAcesso = (Acesso)Enum.Parse(typeof(Acesso), "ADMIN"),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                IdUserUpdate = 1,
                Ativo = true
            }
        );
    }
}
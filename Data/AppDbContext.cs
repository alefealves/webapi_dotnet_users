using Microsoft.EntityFrameworkCore;
using UsersManager.Models;

namespace UsersManager.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options)
    {            
    }

    public DbSet<UserModel> Users { get; set; }
}
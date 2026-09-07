using E_commerce.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infa;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<Usuario> Usuario {get; set;}
}
using E_commerce.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infa;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<Usuario> Usuario {get; set;}
    public DbSet<Cliente> Cliente {get; set;}
    public DbSet<Vendedor> Vendedor {get; set;}
    public  DbSet<Produto> Produto {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasIndex(c => c.Email).IsUnique();
            entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
            entity.Property(u => u.Nome).IsRequired().HasMaxLength(60);
            entity.Property(u => u.Senha).IsRequired().HasMaxLength(255);
            entity.Property(u => u.TipoUsuario).IsRequired().HasConversion<string>();
        });
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasIndex(c => c.Cpf).IsUnique();
            entity.Property(c => c.Cpf).IsRequired().HasMaxLength(11);
            entity.Property(c => c.DataNascimento).IsRequired();
            
            entity.HasOne(c => c.Usuario)
                .WithOne()
                .HasForeignKey<Cliente>(c => c.IdUsuario)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Vendedor>(entity =>
        {
            entity.HasIndex(v => v.Cpf).IsUnique();
            entity.HasIndex(v => v.Cnpj).IsUnique();
            entity.Property(v => v.Cpf).HasMaxLength(11);
            entity.Property(v => v.Cnpj).HasMaxLength(14);
            entity.Property(v => v.NomeLoja).IsRequired().HasMaxLength(100);
            
            entity.HasOne(v => v.Usuario)
                .WithOne()
                .HasForeignKey<Vendedor>(v => v.IdUsuario)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Produto>(entity =>
        {
            entity.Property(p => p.Nome).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Descricao).HasMaxLength(255);
            entity.Property(p => p.Preco).IsRequired().HasColumnType("decimal(18,2)");

            entity.HasOne(p => p.Vendedor)
                .WithMany(v => v.Produtos)
                .HasForeignKey(p => p.IdVendedor)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

  
}
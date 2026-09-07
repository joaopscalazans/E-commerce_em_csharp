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
    public DbSet<Endereco> Endereco {get; set;}
    public DbSet<Pedido> Pedido {get; set;}
    public DbSet<ItemPedido> ItemPedido {get; set;}

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
        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.Property(e => e.Rua).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Numero).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Bairro).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Cep).IsRequired().HasMaxLength(8);
            entity.Property(e => e.Cidade).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Estado).IsRequired().HasMaxLength(2);

            entity.Property(e => e.Tipo).HasMaxLength(30).IsRequired(false);
            entity.Property(e => e.Complemento).HasMaxLength(100).IsRequired(false);

            entity.HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.IdUsuario)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.Property(p => p.DataPedido).IsRequired();
            entity.Property(p => p.ValorTotal).IsRequired().HasColumnType("decimal(18,2)");
            entity.HasOne(p => p.Endereco)
                .WithMany()
                .HasForeignKey(p => p.IdEndereco)
                .IsRequired().OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.IdCliente)
                .IsRequired();
        });
        modelBuilder.Entity<ItemPedido>(entity =>
        {
            entity.Property(ip => ip.PrecoUnitario).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(ip => ip.Quantidade).IsRequired();
            entity.HasOne(ip => ip.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(ip => ip.IdPedido);
            entity.HasOne(ip => ip.Produto)
                .WithMany()
                .HasForeignKey(ip => ip.IdProduto);

            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Quantidade_MaiorQueZero", "\"Quantidade\" > 0");
                t.HasCheckConstraint("CK_PrecoUnitario_MaiorQueZero", "\"PrecoUnitario\" > 0");
            });
        });
    }

  
}
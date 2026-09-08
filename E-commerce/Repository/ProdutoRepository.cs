using E_commerce.Entity.Model;
using E_commerce.Infa;
using E_commerce.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repository;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;
    
    public ProdutoRepository( AppDbContext context) => _context = context;
    
    public  async Task<Produto> ObterPorIdAsync(int id)
    {
        return await _context.Produto.FirstOrDefaultAsync(p =>  p.Id == id);
    }

    public async Task<List<Produto>> ObterTodos()
    {
        return  await _context.Produto.ToListAsync();
    }

    public async Task<List<Produto>> ObterTodosPorVendedorAsync(int idVendedor)
    {
        return await _context.Produto.Where(p => p.IdVendedor == idVendedor).ToListAsync();
    }

    public async Task RegistrarAsync(Produto produto)
    {
        _context.Produto.Add(produto);
        await _context.SaveChangesAsync();
    }

    public Task AtualizarAsync(Produto produto)
    {
        _context.Produto.Update(produto);
        return _context.SaveChangesAsync();
    }
    
    public Task RemoverAsync(Produto produto)
    {
        _context.Produto.Remove(produto);
        return _context.SaveChangesAsync();
    }
}
using E_commerce.Entity.Model;
using E_commerce.Infa;
using E_commerce.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repository;

public class PedidoRepository : IPedidoRepository
{
    
    private readonly AppDbContext _context;
    
    public PedidoRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Pedido>> ObterTodosPorClienteAsync(int idCliente)
    {
        return await _context.Pedido.Where(p => p.IdCliente == idCliente).ToListAsync();
    }

    public async Task<Pedido> ObterPorIdAsync(int idCliente,int idPedido)
    {
        return await _context.Pedido
            .Where(p => p.IdCliente == idCliente)
            .FirstOrDefaultAsync(p => p.Id == idPedido);
    }

    public async Task RegistrarAsync(Pedido pedido)
    {
        _context.Pedido.Add(pedido);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Pedido pedido)
    {
        _context.Pedido.Remove(pedido);
        await _context.SaveChangesAsync();
    }
}
using E_commerce.Entity.Model;
using E_commerce.Infa;
using E_commerce.Repository.Interface;

namespace E_commerce.Repository;

public class ItemPedidoRepository : IItemPedidoRepository
{
    private readonly AppDbContext _context;

    public ItemPedidoRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task RegistarAsync(ItemPedido itemPedido)
    {
       _context.ItemPedido.Add(itemPedido);
       await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(ItemPedido itemPedido)
    {
        _context.ItemPedido.Update(itemPedido);
        await _context.SaveChangesAsync();
    }

    public Task RemoverAsync(ItemPedido itemPedido)
    {
        _context.ItemPedido.Remove(itemPedido);
        return _context.SaveChangesAsync();
    }
}
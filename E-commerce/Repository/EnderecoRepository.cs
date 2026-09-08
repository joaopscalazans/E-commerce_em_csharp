using E_commerce.Entity.Model;
using E_commerce.Infa;
using E_commerce.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repository;

public class EnderecoRepository : IEnderecoRepository
{
    private readonly AppDbContext _context;

    public EnderecoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Endereco?> ObterPorUsuarioAsync(int idUsuario, int idEndereco)
    {
        return await _context.Endereco
            .Include(e => e.Usuario)
            .FirstOrDefaultAsync(e => e.Id == idEndereco && e.IdUsuario == idUsuario);
    }

    public async Task<List<Endereco>> ObterTodosPorUsuarioAsync(int idUsuario)
    {
        return await _context.Endereco
            .Where(e => e.IdUsuario == idUsuario)
            .ToListAsync();
    }

    public async Task RegistrarAsync(Endereco endereco)
    {
        await _context.Endereco.AddAsync(endereco);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Endereco endereco)
    {
        _context.Endereco.Update(endereco);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Endereco endereco)
    {
        _context.Endereco.Remove(endereco);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> PertenceAoCliente(int idUsuario, int idEndereco)
    {
        return await _context.Endereco
            .AnyAsync(e => e.Id == idEndereco && e.IdUsuario == idUsuario);
    }
}
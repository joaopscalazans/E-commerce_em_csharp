using E_commerce.Entity.Model;
using E_commerce.Infa;
using E_commerce.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repository;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context) => _context = context;

    public async Task RegistarAsync(Cliente cliente)
    {
        await _context.Cliente.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Cliente cliente)
    {
        _context.Cliente.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Cliente cliente)
    {
        _context.Cliente.Remove(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task<Cliente> ObterPorIdAsync(int id)
    {
      return await  _context.Cliente
          .Include(c => c.Usuario)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public async Task<bool> ExistePorCpf(string cpf)
    {
        return await _context.Cliente.AnyAsync(c => c.Cpf == cpf);
    } 
    public async Task<bool> ExistePorId(int id)
    {
        return await _context.Cliente.AnyAsync(c => c.Id == id);
    }
    
}
using E_commerce.Entity.Model;
using E_commerce.Infa;
using E_commerce.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repository;

public class VendedorRepository : IVendedorRepository
{
    private readonly AppDbContext _context;
    
    public VendedorRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task RegistarAsync(Vendedor vendedor)
    {
        _context.Add(vendedor);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Vendedor vendedor)
    {
        _context.Update(vendedor);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Vendedor vendedor)
    {
        _context.Remove(vendedor);
        await _context.SaveChangesAsync();
    }

    public async Task<Vendedor> ObterPorIdAsync(int id)
    {
       return await _context.Vendedor.Include(c => c.Usuario).FirstOrDefaultAsync(v => v.Id == id);
    }
    public async Task<bool> ExistePorCpf(string cpf)
    {
        return await _context.Vendedor.AnyAsync(c => c.Cpf == cpf);
    }
    public async Task<bool> ExistePorCnpj(string cnpj)
    {
        return await _context.Vendedor.AnyAsync(c => c.Cpf == cnpj);
    }
    public async Task<bool> ExistePorId(int id)
    {
        return await _context.Vendedor.AnyAsync(c => c.Id == id);
    }
}
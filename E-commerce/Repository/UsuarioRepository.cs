using E_commerce.Entity.Model;
using E_commerce.Infa;
using E_commerce.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    
    private readonly AppDbContext _context;
    
    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> ExistePorEmail(string email)
    {
        return await _context.Usuario.AnyAsync(u => u.Email == email);
    }    public async Task<bool> ExistePorId(int id)
    {
        return await _context.Usuario.AnyAsync(u => u.Id == id);
    }

    public async Task<Usuario> ObterPorId(int id)
    {
        return await _context.Usuario
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}
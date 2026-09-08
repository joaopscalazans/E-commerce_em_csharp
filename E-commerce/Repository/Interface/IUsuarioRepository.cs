using E_commerce.Entity.Model;

namespace E_commerce.Repository.Interface;

public interface IUsuarioRepository
{
    Task<bool> ExistePorEmail(string email);
    Task<bool> ExistePorId(int id);
    Task<Usuario> ObterPorId(int id);
}
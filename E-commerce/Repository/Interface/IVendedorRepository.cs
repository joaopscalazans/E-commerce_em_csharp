using E_commerce.Entity.Model;

namespace E_commerce.Repository.Interface;

public interface IVendedorRepository
{
    Task RegistarAsync(Vendedor vendedor);
    Task AtualizarAsync(Vendedor vendedor);
    Task RemoverAsync(Vendedor vendedor);
    Task<Vendedor> ObterPorIdAsync(int id);
    
    Task<bool> ExistePorCpf(string cpf);
    Task<bool> ExistePorCnpj(string cnpj);
    Task<bool> ExistePorId(int id);
}
using E_commerce.Entity.Model;

namespace E_commerce.Repository.Interface;

public interface IClienteRepository
{
    Task RegistarAsync(Cliente cliente);
    Task AtualizarAsync(Cliente cliente);
    Task RemoverAsync(Cliente cliente);
    Task<Cliente> ObterPorIdAsync(int id);
    
    Task<bool> ExistePorCpf(string cpf);
    Task<bool> ExistePorId(int id);
    

}
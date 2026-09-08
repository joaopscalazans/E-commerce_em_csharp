using E_commerce.Entity.Dto;
using E_commerce.Entity.Model;

namespace E_commerce.Service.Interface;

public interface IClienteService
{
    Task<(bool Registrou,string mensagem)> Registar(ClienteRegistroDto dto);
    Task<(bool Atualizou,string mensagem)> Atualizar(int id, ClienteAlterarDto dto);
    Task<(bool Removou,string mensagem)> RemoverPorId(int id);
    Task<(Cliente? cliente,string mensagem)> ObterPorId(int id);
    
}
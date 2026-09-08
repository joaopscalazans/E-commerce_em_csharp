using E_commerce.Entity.Dto;
using E_commerce.Entity.Model;

namespace E_commerce.Service.Interface;

public interface IVendedorService
{
    Task<(bool Registrou,string? mensagem)> Registar(VendedorRegistroDto vendedor);
    Task<(bool Atualizou,string mensagem)> Atualizar(int id,VendedorAlterarDto vendedor);
    Task<(bool Removeu,string mensagem)> RemoverPorId(int id);
    Task<(Vendedor? vendedor, string? mensagem)> ObterPorId(int id);
}
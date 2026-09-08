using E_commerce.Entity.Dto;
using E_commerce.Entity.Model;

namespace E_commerce.Service.Interface;

public interface IEnderecoService
{
    Task<(Endereco? endereco, string? mensagem)> ObterPorUsuario(int idUsuario, int idEndereco);
    Task<(List<Endereco> enderecos, string? mensagem)> ObterTodosPorUsuario(int idUsuario);
    Task<(bool Registrou, string mensagem)> Registrar(int idUsuario, EnderecoDto dto);
    Task<(bool Atualizou, string mensagem)> Atualizar(int idUsuario, int idEndereco, EnderecoDto dto);
    Task<(bool Removeu, string mensagem)> Remover(int idUsuario, int idEndereco);
}

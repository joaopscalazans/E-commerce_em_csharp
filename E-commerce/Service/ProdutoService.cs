using E_commerce.Entity.Dto;
using E_commerce.Entity.Model;
using E_commerce.Repository.Interface;
using E_commerce.Service.Interface;

namespace E_commerce.Service;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repository;
    private readonly IVendedorRepository _vendedorRepository;

    public ProdutoService(IProdutoRepository repository,
        IVendedorRepository vendedorRepository)
    {
        _repository = repository;
        _vendedorRepository = vendedorRepository;
    }
    
    public async Task<(Produto? produto, string? mensagem)> ObterPorId(int id)
    {
        if (id <= 0)
            return (null, "ID inválido");

        var produto = await _repository.ObterPorIdAsync(id);
        return produto != null 
            ? (produto, null) 
            : (null, "Produto não encontrado");
    }

   
    public async Task<(List<Produto> produtos, string? mensagem)> ObterTodosPorVendedor(int idVendedor)
    {
        if (idVendedor <= 0)
            return (new List<Produto>(), "ID do vendedor inválido");

        var produtos = await _repository.ObterTodosPorVendedorAsync(idVendedor);
        return (produtos, null);
    }

    public async Task<(bool Registrou, string? mensagem)> Registrar(int idVendedor,ProdutoDto dto)
    {
        if (dto == null)
            return (false, "Produto não pode ser nulo");

        if (string.IsNullOrWhiteSpace(dto.Nome))
            return (false, "Nome do produto é obrigatório");

        if (dto.Preco <= 0)
            return (false, "Preço deve ser maior que zero");
        if (idVendedor <= 0 || !await _vendedorRepository.ExistePorId(idVendedor))
            return (false, "Vendedor não encontrado");
        var produto = new Produto(idVendedor, dto.Nome,dto.Descricao, dto.Preco);

        await _repository.RegistrarAsync(produto);
        return (true, "Produto registrado com sucesso");
    }


    public async Task<(bool Atualizou, string mensagem)> Atualizar(int id, ProdutoDto dto)
    {
        if (id <= 0)
            return (false, "ID inválido");

        if (dto == null)
            return (false, "Produto não pode ser nulo");
        
        if (string.IsNullOrWhiteSpace(dto.Nome))
            return (false, "Nome do produto é obrigatório");

        if (dto.Preco <= 0)
            return (false, "Preço deve ser maior que zero");

        var produtoExistente = await _repository.ObterPorIdAsync(id);
        if (produtoExistente == null)
            return (false, "Produto não encontrado");

        
        produtoExistente.Nome = dto.Nome;
        produtoExistente.Descricao = dto.Descricao;
        produtoExistente.Preco = dto.Preco;

        await _repository.AtualizarAsync(produtoExistente);
        return (true, "Produto atualizado com sucesso");
    }


    public async Task<(bool Removeu, string mensagem)> RemoverPorId(int id)
    {
        if (id <= 0)
            return (false, "ID inválido");

        var produto = await _repository.ObterPorIdAsync(id);
        if (produto == null)
            return (false, "Produto não encontrado");

        await _repository.RemoverAsync(produto);
        return (true, "Produto removido com sucesso");
    }
}
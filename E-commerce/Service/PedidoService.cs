using E_commerce.Entity.Dto;
using E_commerce.Entity.Model;
using E_commerce.Repository.Interface;
using E_commerce.Service.Interface;

namespace E_commerce.Service;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _repository;
    private readonly IItemPedidoRepository _itemPedidoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IEnderecoRepository _enderecoRepository;

    public PedidoService(IPedidoRepository repository,
        IItemPedidoRepository itemPedidoRepository,
        IClienteRepository clienteRepository,
        IEnderecoRepository enderecoRepository)
    {
        _repository = repository;
        _itemPedidoRepository = itemPedidoRepository;
        _clienteRepository = clienteRepository;
        _enderecoRepository = enderecoRepository;
    }
    
    public async Task<(IEnumerable<Pedido>? Pedidos, string? Mensagem)> ObterTodosPorCliente(int idCliente)
    {
        if ( idCliente <= 0 || await _clienteRepository.ExistePorId(idCliente))
            return (new List<Pedido>(), "Cliente inválido");
        

        var pedidos = await _repository.ObterTodosPorClienteAsync(idCliente);
        return (pedidos, null);
    }

    
    public async Task<(Pedido? Pedido, string? Mensagem)> ObterPorClienteEPedido(int idCliente,int idPedido)
    {
        if (idCliente <= 0 || idPedido <= 0)
            return (null, "ID inválido");
        
        var cliente = await _clienteRepository.ObterPorIdAsync(idCliente);
        
        var pedido = await _repository.ObterPorIdAsync(cliente.Id, idPedido);
        return pedido != null 
            ? (pedido, null) 
            : (null, "Pedido não encontrado");
    }
    
    public async Task<(bool Registrou, string? Mensagem)> Registrar(int idCliente,PedidoRegistroDto dto)
    {
        if (dto == null)
            return (false, "Pedido não pode ser nulo");
        
        if (idCliente <= 0)return (false, "Cliente invalido");
            

        var usuario = await _clienteRepository.ObterPorIdAsync(idCliente);
        
        if (usuario == null) return (false, "Cliente invalido");
        
        if (dto.IdEnderecoEntrega <= 0 || !await _enderecoRepository.PertenceAoCliente(usuario.IdUsuario, dto.IdEnderecoEntrega) )
            return (false, "Endereço invalido");

        if (dto.Itens == null || dto.Itens.Count == 0)
            return (false, "Pedido deve conter pelo menos um item");

        var pedido = new Pedido(idCliente: idCliente, idEndereco: dto.IdEnderecoEntrega);
        
        foreach (var itemDto in dto.Itens)
        {
            if (itemDto.Quantidade <= 0 || itemDto.PrecoUnitario <= 0)
                return (false, "Existem itens com quantidade ou valor inválido");

            pedido.Itens.Add(new ItemPedido
            (
                idProduto: itemDto.IdProduto,
                quantidade:  itemDto.Quantidade,
                precoUnitario: itemDto.PrecoUnitario
            ));
        }

        if (pedido.ValorTotal < 0) return (false, "Pedido contem itens invalidos");
        
        await _repository.RegistrarAsync(pedido);
        
        return (true, "Pedido registrado com sucesso");
    }

    public async Task<(bool Cancelou, string Mensagem)> CancelarPedido(int idCliente, int idPedido)
    {
        if (idCliente <= 0 || idPedido <= 0)
            return (false, "ID inválido");

        var cliente = await _clienteRepository.ObterPorIdAsync(idCliente);
        var pedido = await _repository.ObterPorIdAsync(cliente.Id, idPedido);
        if (pedido == null)
            return (false, "Pedido não encontrado");
        
        foreach (var item in pedido.Itens)
        {
            await _itemPedidoRepository.RemoverAsync(item);
        }

        await _repository.RemoverAsync(pedido);
        return (true, "Pedido cancelado com sucesso");
    }
}
using E_commerce.Entity.Dto;
using E_commerce.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controller;

[ApiController]
[Route("api/clientes/{idCliente:int}/pedidos")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _service;

    public PedidoController(IPedidoService service)
    {
        _service = service;
    }

    // GET: api/clientes/{idCliente}/pedidos
    [HttpGet]
    public async Task<ActionResult> ObterTodosPorCliente(int idCliente)
    {
        var (pedidos, mensagem) = await _service.ObterTodosPorCliente(idCliente);
        return mensagem != null ? BadRequest(new { mensagem }) : Ok(pedidos);
    }

    // GET: api/clientes/{idCliente}/pedidos/{idPedido}
    [HttpGet("{idPedido:int}")]
    public async Task<ActionResult> ObterPorId(int idCliente, int idPedido)
    {
        var (pedido, mensagem) = await _service.ObterPorClienteEPedido(idCliente, idPedido);
        return pedido is null ? NotFound(new { mensagem }) : Ok(pedido);
    }

    // POST: api/clientes/{idCliente}/pedidos
    [HttpPost]
    public async Task<ActionResult> Post(int idCliente, [FromBody] PedidoRegistroDto dto)
    {
        var (registrou, mensagem) = await _service.Registrar(idCliente, dto);
        return registrou ? Created(string.Empty, new { mensagem }) : BadRequest(new { mensagem });
    }

    // DELETE: api/clientes/{idCliente}/pedidos/{idPedido}
    [HttpDelete("{idPedido:int}")]
    public async Task<ActionResult> Cancelar(int idCliente, int idPedido)
    {
        var (cancelou, mensagem) = await _service.CancelarPedido(idCliente, idPedido);
        return cancelou ? Ok(new { mensagem }) : NotFound(new { mensagem });
    }
}
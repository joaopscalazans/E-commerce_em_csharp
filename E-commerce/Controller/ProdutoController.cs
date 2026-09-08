using E_commerce.Entity.Dto;
using E_commerce.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _service;

    public ProdutoController(IProdutoService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> ObterPorId(int id)
    {
        var (produto, mensagem) = await _service.ObterPorId(id);
        return produto is null ? NotFound(new { mensagem }) : Ok(produto);
    }

    [HttpGet("vendedor/{idVendedor:int}")]
    public async Task<ActionResult> ObterPorVendedor(int idVendedor)
    {
        var (produtos, mensagem) = await _service.ObterTodosPorVendedor(idVendedor);
        return mensagem != null ? BadRequest(new { mensagem }) : Ok(produtos);
    }

    [HttpPost("vendedor/{idVendedor:int}")]
    public async Task<ActionResult> Post(int idVendedor, [FromBody] ProdutoDto dto)
    {
        var (registrou, mensagem) = await _service.Registrar(idVendedor, dto);
        return registrou ? Created(string.Empty, new { mensagem }) : BadRequest(new { mensagem });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] ProdutoDto dto)
    {
        var (atualizou, mensagem) = await _service.Atualizar(id, dto);
        return atualizou ? Ok(new { mensagem }) : BadRequest(new { mensagem });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (removeu, mensagem) = await _service.RemoverPorId(id);
        return removeu ? Ok(new { mensagem }) : NotFound(new { mensagem });
    }
}
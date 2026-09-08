using E_commerce.Entity.Dto;
using E_commerce.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controller;

[ApiController]
[Route("api/[controller]")]
public class VendedorController : ControllerBase
{
    private readonly IVendedorService _service;

    public VendedorController(IVendedorService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] VendedorRegistroDto dto)
    {
        var (registrou, mensagem) = await _service.Registar(dto);
        return registrou ? Created(string.Empty, new { mensagem }) : BadRequest(new { mensagem });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> ObterPorId(int id)
    {
        var (vendedor, mensagem) = await _service.ObterPorId(id);
        return vendedor is null ? NotFound(new { mensagem }) : Ok(vendedor);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] VendedorAlterarDto dto)
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
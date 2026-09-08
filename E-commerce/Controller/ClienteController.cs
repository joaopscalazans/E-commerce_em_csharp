using E_commerce.Entity.Dto;
using E_commerce.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controller;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _service;

    public ClienteController(IClienteService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ClienteRegistroDto dto)
    {
        var (registrou, mensagem) = await _service.Registar(dto);
        return registrou ? Created(string.Empty, new { mensagem }) : BadRequest(new { mensagem });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> ObterPorId(int id)
    {
        var (cliente, mensagem) = await _service.ObterPorId(id);
        return cliente is null ? NotFound(new { mensagem }) : Ok(cliente);
    }
}


using E_commerce.Entity.Dto;
using E_commerce.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controller;


[ApiController]
[Route("api/usuarios/{idUsuario:int}/enderecos")]
public class EnderecoController : ControllerBase
{
    private readonly IEnderecoService _service;

    public EnderecoController(IEnderecoService service)
    {
        _service = service;
    }

    [HttpGet("{idEndereco:int}")]
    public async Task<ActionResult> ObterPorId(int idUsuario, int idEndereco)
    {
        var (endereco, mensagem) = await _service.ObterPorUsuario(idUsuario, idEndereco);
        return endereco is null ? NotFound(new { mensagem }) : Ok(endereco);
    }

    [HttpGet]
    public async Task<ActionResult> ObterTodos(int idUsuario)
    {
        var (enderecos, mensagem) = await _service.ObterTodosPorUsuario(idUsuario);
        return mensagem != null ? BadRequest(new { mensagem }) : Ok(enderecos);
    }

    [HttpPost]
    public async Task<ActionResult> Post(int idUsuario, [FromBody] EnderecoDto dto)
    {
        var (registrou, mensagem) = await _service.Registrar(idUsuario, dto);
        return registrou ? Created(string.Empty, new { mensagem }) : BadRequest(new { mensagem });
    }

    [HttpPut("{idEndereco:int}")]
    public async Task<ActionResult> Put(int idUsuario, int idEndereco, [FromBody] EnderecoDto dto)
    {
        var (atualizou, mensagem) = await _service.Atualizar(idUsuario, idEndereco, dto);
        return atualizou ? Ok(new { mensagem }) : BadRequest(new { mensagem });
    }

    [HttpDelete("{idEndereco:int}")]
    public async Task<ActionResult> Delete(int idUsuario, int idEndereco)
    {
        var (removeu, mensagem) = await _service.Remover(idUsuario, idEndereco);
        return removeu ? Ok(new { mensagem }) : NotFound(new { mensagem });
    }
}
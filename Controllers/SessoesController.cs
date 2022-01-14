using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoCinema.Infraestrutura;
using AplicacaoCinema.Dominio;
using AplicacaoCinema.Models;

namespace AplicacaoCinema.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SessoesController : ControllerBase
  {
    private readonly SessoesRepositorio _sessoesRepositorio;

    public SessoesController(SessoesRepositorio sessoesRepositorio)
    {
      _sessoesRepositorio = sessoesRepositorio;
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarAsync([FromBody] NovaSessaoInputModel sessaoInputModel, CancellationToken cancellationToken)
    {
      var sessao = Sessao.Criar(sessaoInputModel.Filme, sessaoInputModel.Inicio, sessaoInputModel.Fim, sessaoInputModel.QuantidadeIngressosTotal, sessaoInputModel.Preco);
      if (sessao.IsFailure)
        return BadRequest(sessao.Error);

      await _sessoesRepositorio.InserirAsync(sessao.Value, cancellationToken);
      await _sessoesRepositorio.CommitAsync(cancellationToken);
      return CreatedAtAction("RecuperarPorId", new { id = sessao.Value.Id }, sessao.Value.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(string id, [FromBody] AlterarSessaoInputModel sessaoInputModel, CancellationToken cancellationToken)
    {
      if (!Guid.TryParse(id, out var guid))
        return BadRequest("Id inválido");
      var sessao = await _sessoesRepositorio.RecuperarPorIdAsync(guid, cancellationToken);
      if (sessao == null)
        return NotFound();

      _sessoesRepositorio.Alterar(sessao);
      await _sessoesRepositorio.CommitAsync(cancellationToken);

      return Ok(sessao);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> RecuperarPorIdAsync(string id, CancellationToken cancellationToken)
    {
      if (!Guid.TryParse(id, out var guid))
        return BadRequest("Id inválido");
      var sessao = await _sessoesRepositorio.RecuperarPorIdAsync(guid, cancellationToken);
      if (sessao == null)
        return NotFound();
      return Ok(sessao);
    }
  }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoCinema.Infraestrutura;
using AplicacaoCinema.Dominio;
using AplicacaoCinema.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace AplicacaoCinema.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class IngressosController : ControllerBase
  {
    private readonly FilmesRepositorio _filmesRepositorio;
    private readonly SessoesRepositorio _sessoesRepositorio;
    private readonly IngressosRepositorio _ingressosRepositorio;
    private readonly ILogger<IngressosController> _logger;

    public IngressosController(
        FilmesRepositorio filmesRepositorio,
        SessoesRepositorio sessoesRepositorio,
        IngressosRepositorio ingressosRepositorio,
        ILogger<IngressosController> logger)
    {
      _filmesRepositorio = filmesRepositorio;
      _sessoesRepositorio = sessoesRepositorio;
      _ingressosRepositorio = ingressosRepositorio;
      _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> ComprarAsync([FromBody] NovaIngressoInputModel inputModel, CancellationToken cancellationToken)
    {

      if (!Guid.TryParse(inputModel.SessaoId, out var guidSessao))
        return BadRequest("Id de sessao inválido");

      var sessao = await _sessoesRepositorio.RecuperarPorIdAsync(guidSessao, cancellationToken);
      if (sessao == null)
        return BadRequest("Sessao não foi localizada");

      sessao.QuantidadeIngressosVendidos = await _ingressosRepositorio.RecuperarNumeroIngressos(guidSessao);

      if (!sessao.AceitaNovosIngressos(inputModel.Quantidade))
        return UnprocessableEntity("O limite da sessão será atingido com esta quantidade de ingressos");

      var ingressos = Ingresso.Criar(sessao.IdSessao, DateTime.UtcNow, inputModel.Quantidade);
      if (ingressos.IsFailure)
        return BadRequest(ingressos.Error);

      foreach (var ingresso in ingressos.Value)
      {
        await _ingressosRepositorio.InserirAsync(ingresso, cancellationToken);
        await _ingressosRepositorio.CommitAsync(cancellationToken);
      }
      return CreatedAtAction("RecuperarPorSessao", new { id = guidSessao }, ingressos.Value);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> RecuperarPorIdAsync(string id, CancellationToken cancellationToken)
    {
      if (!Guid.TryParse(id, out var guid))
        return BadRequest("Id inválido");
      var ingresso = await _ingressosRepositorio.RecuperarPorIdAsync(guid, cancellationToken);
      if (ingresso == null)
        return NotFound();
      return Ok(ingresso);
    }

    [HttpGet("Sessoes/{id}")]
    public async Task<IActionResult> RecuperarPorSessaoAsync(string id, CancellationToken cancellationToken)
    {
      if (!Guid.TryParse(id, out var guid))
        return BadRequest("Id inválido");
      var ingressos = await _ingressosRepositorio.RecuperarPorSessaoAsync(guid, cancellationToken);
      if (ingressos == null)
        return NotFound();
      return Ok(ingressos);
    }

    [HttpGet]
    public async Task<IActionResult> RecuperarTodosAsync(CancellationToken cancellationToken = default)
    {
      var ingressos = await _ingressosRepositorio.RecuperarTodosAsync(cancellationToken);
      if (ingressos == null)
        return NotFound();
      return Ok(ingressos);
    }
  }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoCinema.Infraestrutura;
using AplicacaoCinema.Dominio;
using AplicacaoCinema.Models;
using Microsoft.Extensions.Logging;

namespace AplicacaoCinema.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SessoesController : ControllerBase
  {
    private readonly SessoesRepositorio _sessoesRepositorio;
    private readonly FilmesRepositorio _filmesRepositorio;
    private readonly IngressosRepositorio _ingressosRepositorio;
    private readonly ILogger<IngressosController> _logger;


    public SessoesController(
        SessoesRepositorio sessoesRepositorio,
        FilmesRepositorio filmesRepositorios,
        IngressosRepositorio ingressosRepositorio,
        ILogger<IngressosController> logger
        )
    {
      _sessoesRepositorio = sessoesRepositorio;
      _filmesRepositorio = filmesRepositorios;
      _ingressosRepositorio = ingressosRepositorio;
      _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarAsync([FromBody] NovaSessaoInputModel sessaoInputModel, CancellationToken cancellationToken)
    {
      try
      {

        var sessao = Sessao.Criar(sessaoInputModel.IdFilme, sessaoInputModel.Inicio, sessaoInputModel.Fim, sessaoInputModel.QuantidadeIngressosTotal, sessaoInputModel.Preco);
        if (sessao.IsFailure)
          return BadRequest(sessao.Error);

        await _sessoesRepositorio.InserirAsync(sessao.Value, cancellationToken);
        await _sessoesRepositorio.CommitAsync(cancellationToken);
        return CreatedAtAction("RecuperarPorId", new { id = sessao.Value.IdSessao }, sessao.Value.IdSessao);
      }
      catch (System.Exception ex)
      {

        return BadRequest(ex.Message);
      }
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

      var filme = await _filmesRepositorio.RecuperarPorIdAsync(sessao.IdFilme, cancellationToken);
      sessao.Fim = sessao.Inicio.AddMinutes(filme.Duracao);
      return Ok(sessao);
    }

    [HttpGet]
    public async Task<IActionResult> RecuperarTodosAsync(CancellationToken cancellationToken)
    {
      var sessoes = await _sessoesRepositorio.RecuperarTodosAsync(cancellationToken);
      if (sessoes == null)
        return NotFound();

      var sessao = sessoes.ToList();

      sessao.ForEach((ses) =>
     {
       var filme = _filmesRepositorio.RecuperarPorIdAsync(ses.IdFilme, cancellationToken).GetAwaiter();
       ses.Fim = ses.Inicio.AddMinutes(filme.GetResult().Duracao);
       ses.QuantidadeIngressosVendidos = _ingressosRepositorio.RecuperarNumeroIngressos(ses.IdSessao).GetAwaiter().GetResult();
     });
      return Ok(sessao);
    }
  }
}

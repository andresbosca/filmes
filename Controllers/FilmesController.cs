using System;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoCinema.Dominio;
using AplicacaoCinema.Infraestrutura;
using AplicacaoCinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AplicacaoCinema.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FilmesController : ControllerBase
  {
    private readonly ILogger<FilmesController> _logger;
    private readonly FilmesRepositorio _filmesRepositorio;

    public FilmesController(
        ILogger<FilmesController> logger,
        FilmesRepositorio filmesRepositorio)
    {
      _logger = logger;
      _filmesRepositorio = filmesRepositorio;
    }

    [HttpPost]
    public async Task<IActionResult> IncluirAsync([FromBody] NovoFilmeInputModel inputModel, CancellationToken cancellationToken)
    {

      var filme = Filme.Criar(inputModel.Titulo, inputModel.Duracao, inputModel.Sinopse);
      if (filme.IsFailure)
        return BadRequest(filme.Error);

      if (await _filmesRepositorio.RecuperarPorNomeAsync(filme.Value.Titulo) != null)
        return BadRequest("Filme já existe");


      string message = $"Filme {filme.Value} criado em memória { filme.Value.Id}";
      _logger.LogInformation(message);

      await _filmesRepositorio.InserirAsync(filme.Value, cancellationToken);
      await _filmesRepositorio.CommitAsync(cancellationToken);
      return CreatedAtAction("RecuperarPorId", new { id = filme.Value.Id, cancellationToken = cancellationToken }, filme.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
      var filme = await _filmesRepositorio.RecuperarPorIdAsync(id, cancellationToken);

      return Ok(filme);
    }

    [HttpGet]
    public async Task<IActionResult> RecuperarTodosAsync(CancellationToken cancellationToken)
    {
      var filme = await _filmesRepositorio.RecuperarTodosAsync(cancellationToken);

      return Ok(filme);
    }

  }
}
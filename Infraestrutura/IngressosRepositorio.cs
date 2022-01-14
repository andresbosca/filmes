using System;
using System.Collections.Generic;
using AplicacaoCinema.Dominio;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AplicacaoCinema.Infraestrutura
{
  public sealed class IngressosRepositorio
  {
    private readonly CinemasDbContext _cinemasDbContext;
    private readonly IConfiguration _configuracao;


    public IngressosRepositorio(CinemasDbContext cinemasDbContext, IConfiguration configuracao)
    {
      _cinemasDbContext = cinemasDbContext;
      _configuracao = configuracao;
    }

    public async Task InserirAsync(Ingresso ingresso, CancellationToken cancellationToken = default)
    {
      await _cinemasDbContext.AddAsync(ingresso, cancellationToken);
    }

    public async Task<Ingresso> RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
      return await _cinemasDbContext
          .Ingressos
          .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
    public async Task<IEnumerable<Ingresso>> RecuperarTodosAsync(Guid idSessao, CancellationToken cancellationToken = default)
    {
      return await _cinemasDbContext
          .Ingressos
          .Include(c => c.Sessao.Id == idSessao)
          .ToListAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
      await _cinemasDbContext.SaveChangesAsync(cancellationToken);
    }
  }
}

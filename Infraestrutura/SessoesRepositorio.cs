using System;
using System.Collections.Generic;
using AplicacaoCinema.Dominio;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AplicacaoCinema.Infraestrutura
{
  public sealed class SessoesRepositorio
  {
    private readonly CinemasDbContext _cinemasDbContext;
    private readonly IConfiguration _configuracao;


    public SessoesRepositorio(CinemasDbContext cinemasDbContext, IConfiguration configuracao)
    {
      _cinemasDbContext = cinemasDbContext;
      _configuracao = configuracao;
    }

    public async Task InserirAsync(Sessao sessao, CancellationToken cancellationToken = default)
    {
      await _cinemasDbContext.AddAsync(sessao, cancellationToken);
    }

    public void Alterar(Sessao sessao)
    {
      // Nada a fazer EF CORE fazer o Tracking da Entidade quando recuperamos a mesma
    }

    public async Task<IEnumerable<Sessao>> RecuperarTodosAsync(CancellationToken cancellationToken = default)
    {
      return await _cinemasDbContext
          .Sessoes
          .Include(c => c.Id)
          .ToListAsync(cancellationToken);
    }

    public async Task<Sessao> RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
      return await _cinemasDbContext
          .Sessoes
          .Include(c => c.Id)
          .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
      await _cinemasDbContext.SaveChangesAsync(cancellationToken);
    }
  }
}

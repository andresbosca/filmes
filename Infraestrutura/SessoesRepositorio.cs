using System;
using System.Collections.Generic;
using AplicacaoCinema.Dominio;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

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
      var sessoes = await _cinemasDbContext
          .Sessoes
          .ToListAsync<Sessao>(cancellationToken);

      return sessoes;
    }


    public async Task<Sessao> RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
      return await _cinemasDbContext
          .Sessoes
          .FirstOrDefaultAsync(c => c.IdSessao == id, cancellationToken);
    }
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
      await _cinemasDbContext.SaveChangesAsync(cancellationToken);
    }
  }
}

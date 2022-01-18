using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoCinema.Dominio;
using Microsoft.EntityFrameworkCore;

namespace AplicacaoCinema.Infraestrutura
{
  public sealed class FilmesRepositorio
  {
    private readonly CinemasDbContext _dbContext;


    public FilmesRepositorio(CinemasDbContext dbContext)
    {
      _dbContext = dbContext;
    }
    public async Task InserirAsync(Filme novoFilme, CancellationToken cancellationToken = default)
    {
      await _dbContext.Filmes.AddAsync(novoFilme, cancellationToken);
    }
    public async Task<Filme> RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
      return await _dbContext
                      .Filmes
                      .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
    public async Task<Filme> RecuperarPorNomeAsync(String nome, CancellationToken cancellationToken = default)
    {
      return await _dbContext
                      .Filmes
                      .FirstOrDefaultAsync(c => c.Titulo == nome, cancellationToken);
    }
    public async Task<IEnumerable<Filme>> RecuperarTodosAsync(CancellationToken cancellationToken = default)
    {
      return await _dbContext
                      .Filmes
                      .ToListAsync<Filme>(cancellationToken);
    }
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
      await _dbContext.SaveChangesAsync(cancellationToken);
    }

  }
}
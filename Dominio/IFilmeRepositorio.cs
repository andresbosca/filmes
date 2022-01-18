using System;
using System.Threading;
using System.Threading.Tasks;

namespace AplicacaoCinema.Dominio
{
  public interface IFilmeRepositorio<T>
  {
    Task<Filme> RecuperarPorTituloAsync(string titulo, CancellationToken cancellationToken = default);
  }
}
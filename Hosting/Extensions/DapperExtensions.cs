using AplicacaoCinema.Infraestrutura.Mappers;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace AplicacaoCinema.Hosting.Extensions
{
  public static class DapperExtensions
  {
    public static IServiceCollection AddDapper(this IServiceCollection serviceCollection)
    {
      SqlMapper.AddTypeHandler(new GuidTypeHandler());
      return serviceCollection;
    }
  }
}
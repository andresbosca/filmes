using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Serilog;

namespace AplicacaoCinema
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // The initial "bootstrap" logger is able to log errors during start-up. It's completely replaced by the
      // logger configured in `UseSerilog()` below, once configuration and dependency-injection have both been
      // set up successfully.
      Log.Logger = new LoggerConfiguration()
          .WriteTo.Console()
          .CreateBootstrapLogger();

      Log.Information("Iniciando Aplicação");
 
      try
      {
        CreateHostBuilder(args).Build().Run();

        Log.Information("Aplicação finalizou com sucesso");
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "Erro ao carregar a Aplicação");
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Seq("http://localhost:5341")
                .WriteTo.Console())
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}

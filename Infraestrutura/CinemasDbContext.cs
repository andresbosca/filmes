using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoCinema.Dominio;
using AplicacaoCinema.Infraestrutura.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace AplicacaoCinema.Infraestrutura
{
  public class CinemasDbContext : DbContext
  {
    public CinemasDbContext(DbContextOptions options)
        : base(options)
    {

    }

    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Sessao> Sessoes { get; set; }
    public DbSet<Ingresso> Ingressos { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      try
      {
        foreach (var item in ChangeTracker.Entries())
        {
          if (item.State is Microsoft.EntityFrameworkCore.EntityState.Modified or Microsoft.EntityFrameworkCore.EntityState.Added
              && item.Properties.Any(c => c.Metadata.Name == "DataUltimaAlteracao"))
            item.Property("DataUltimaAlteracao").CurrentValue = DateTime.UtcNow;

          if (item.State == EntityState.Added
              && item.Properties.Any(c => c.Metadata.Name == "DataCadastro"))
            item.Property("DataCadastro").CurrentValue = DateTime.UtcNow;
        }
        return await base.SaveChangesAsync(cancellationToken);
      }
      catch (DbUpdateException)
      {
        throw;
      }
      catch (Exception)
      {
        throw;
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new FilmeTypeConfiguration());
      modelBuilder.ApplyConfiguration(new SessoesTypeConfiguration());
      modelBuilder.ApplyConfiguration(new IngressoTypeConfiguration());
    }
  }
}
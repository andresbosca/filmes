using AplicacaoCinema.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AplicacaoCinema.Infraestrutura.EntityConfigurations
{
  public sealed class IngressoTypeConfiguration : IEntityTypeConfiguration<Ingresso>
  {
    public void Configure(EntityTypeBuilder<Ingresso> builder)
    {

      builder.ToTable("Ingressos", "Cinemas");
      builder.HasKey(c => c.Id);
      builder.HasKey(c => c.Sessao.Id);
      builder.OwnsOne(c => c.Sessao);

    }
  }
}
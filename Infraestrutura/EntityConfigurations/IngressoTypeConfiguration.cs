using System;
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

      builder.ToTable("Ingressos", "dbo");
      builder.HasKey(c => c.Id);
      builder.Property(c => c.Data);
      builder.Property(c => c.SessaoId);

    }
  }
}
using System;
using AplicacaoCinema.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AplicacaoCinema.Infraestrutura.EntityConfigurations
{
  public sealed class FilmeTypeConfiguration : IEntityTypeConfiguration<Filme>
  {
    public void Configure(EntityTypeBuilder<Filme> builder)
    {
      builder.ToTable("Filmes", "dbo");
      builder.HasKey(c => c.Id);
      builder.Property(c => c.Titulo);
      builder.Property(c => c.Sinopse);
      builder.Property(c => c.Duracao);

    }
  }
}
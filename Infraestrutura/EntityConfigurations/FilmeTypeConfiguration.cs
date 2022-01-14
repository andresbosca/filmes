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
      builder.ToTable("Filmes", "Cinemas");
      builder.HasKey(c => c.Id);
      builder.HasKey(c => c.Titulo);
      builder.Property(c => c.Sinopse);
      builder.Property(c => c.Duracao);
    }
  }
}
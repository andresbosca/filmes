using System;
using AplicacaoCinema.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AplicacaoCinema.Infraestrutura.EntityConfigurations
{
  public sealed class SessaoTypeConfiguration : IEntityTypeConfiguration<Sessao>
  {
    public void Configure(EntityTypeBuilder<Sessao> builder)
    {
      builder.ToTable("Sessoes", "Cinemas");
      builder.HasKey(c => c.Id);
      builder.Property(c => c._filme);
      builder.Property(c => c._fim);
      builder.Property(c => c._inicio);
      builder.Property(c => c._preco);
      builder.Property(c => c._quantidadeIngressosTotal);
      builder.Property(c => c._quantidadeIngressosVendidos);
    }
  }
}
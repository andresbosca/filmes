using AplicacaoCinema.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AplicacaoCinema.Infraestrutura.EntityConfigurations
{
  public sealed class SessoesTypeConfiguration : IEntityTypeConfiguration<Sessao>
  {
    public void Configure(EntityTypeBuilder<Sessao> builder)
    {
      builder.ToTable("Sessoes", "dbo");
      builder.HasKey(c => c.IdSessao).HasName("Id");
      builder.Property(c => c.IdSessao).HasColumnName("Id");
      builder.Property(c => c.IdFilme).HasColumnName("FilmeId");
      builder.Ignore(c => c.Fim);
      builder.Property(c => c.Inicio).HasColumnName("DataInicio");
      builder.Property(c => c.Preco).HasColumnName("Valor");
      builder.Property(c => c.QuantidadeIngressosTotal).HasColumnName("QuantidadeMaxima");
      builder.Ignore(c => c.QuantidadeIngressosVendidos);
    }
  }
}
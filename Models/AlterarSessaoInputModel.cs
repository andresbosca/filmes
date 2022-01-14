using System;
using System.ComponentModel.DataAnnotations;
using AplicacaoCinema.Dominio;

namespace AplicacaoCinema.Models
{
  public sealed class AlterarSessaoInputModel
  {
    [Required]
    [MinLength(5, ErrorMessage = "Tamanho inválido")]
    public Guid Id;
    public Filme Filme;
    public DateTime Inicio;
    public DateTime Fim;
    public int QuantidadeIngressosTotal;
    public double Preco;
  }
}
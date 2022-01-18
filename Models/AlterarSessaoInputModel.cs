using System;
using System.ComponentModel.DataAnnotations;
using AplicacaoCinema.Dominio;

namespace AplicacaoCinema.Models
{
  public sealed class AlterarSessaoInputModel
  {
    [Required]
    [MinLength(5, ErrorMessage = "Tamanho inválido")]
    public Guid Id { get; set; }
    public Guid IdFilme { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public int QuantidadeIngressosTotal { get; set; }
    public double Preco { get; set; }
  }
}
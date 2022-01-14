using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AplicacaoCinema.Dominio;

namespace AplicacaoCinema.Models
{
  public sealed class NovaSessaoInputModel
  {
    [Required]
    [MinLength(5, ErrorMessage = "Tamanho inválido")]
    public Filme Filme;
    [Required]
    public DateTime Inicio;
    [Required]
    public DateTime Fim;
    [Required]
    public int QuantidadeIngressosTotal;
    [Required]
    public double Preco;
  }
}
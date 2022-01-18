using System;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoCinema.Models
{
  public sealed class NovaSessaoInputModel
  {
    [Required(ErrorMessage = "Informe um Filme")]
    public Guid IdFilme { get; set; }
    [Required(ErrorMessage = "Informe uma data de inicio")]
    public DateTime Inicio { get; set; }
    [Required]
    public DateTime Fim { get; set; }
    [Required]
    public int QuantidadeIngressosTotal { get; set; }
    [Required]
    public Double Preco { get; set; }
  }
}
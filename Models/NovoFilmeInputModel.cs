using System;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoCinema.Models
{
  public class NovoFilmeInputModel
  {
    [Required]
    public string Titulo { get; }
    [Required]
    public int Duracao { get; }
    [Required]
    public string Sinopse { get; }
  }
}
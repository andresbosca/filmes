using System;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoCinema.Models
{
  public class NovoFilmeInputModel
  {
    [Required(ErrorMessage = "Título não pode ser em branco")] public string Titulo { get; set; }

    [Required(ErrorMessage = "Duracao não pode ser null")]
    [Range(1, 600)] public int Duracao { get; set; }
    [Required(ErrorMessage = "Sinopse não pode ser em branco")] public string Sinopse { get; set; }
  }
}
using System.ComponentModel.DataAnnotations;

namespace AplicacaoCinema.Models
{
  public class NovaIngressoInputModel
  {
    [Required(ErrorMessage = "Informe uma sessão")]
    public string SessaoId { get; set; }

    [Required(ErrorMessage = "Informe uma quantidade")]
    public int Quantidade { get; set; }
  }
}
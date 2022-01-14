namespace AplicacaoCinema.Hosting.Configuration
{
  public class IngressosOptions
  {
    public const string Ingressos = "Ingressos";

    public bool Aberto { get; set; } = false;
    public string MensagemSucesso { get; set; } = "Venda realizada com sucesso";

  }
}

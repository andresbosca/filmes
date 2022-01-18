using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CSharpFunctionalExtensions;

namespace AplicacaoCinema.Dominio
{
  public sealed class Sessao
  {
    public Guid IdSessao { get; set; }
    public Guid IdFilme { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public int QuantidadeIngressosTotal { get; set; }
    public Double Preco { get; set; }
    public int QuantidadeIngressosVendidos { get; set; }
    private Sessao() { }
    public Sessao(Guid id, Guid idFilme, DateTime inicio, DateTime fim, int quantidadeIngressosTotal, Double preco)
    {
      IdSessao = id;
      IdFilme = idFilme;
      Inicio = inicio;
      Fim = fim;
      QuantidadeIngressosTotal = quantidadeIngressosTotal;
      Preco = preco;
      QuantidadeIngressosVendidos = 0;
    }
    public Sessao(Guid id, Guid idFilme, DateTime inicio, DateTime fim, int quantidadeIngressosTotal, Double preco, int quantidadeIngressosVendidos)
    {
      IdSessao = id;
      IdFilme = idFilme;
      Inicio = inicio;
      Fim = fim;
      QuantidadeIngressosTotal = quantidadeIngressosTotal;
      Preco = preco;
      QuantidadeIngressosVendidos = quantidadeIngressosVendidos;
    }
    public bool AceitaNovosIngressos(int quantidade)
    {
      return QuantidadeIngressosTotal >= (QuantidadeIngressosVendidos + quantidade);
    }

    public static Result<Sessao> Criar(Guid filme, DateTime inicio, DateTime fim, int quantidadeIngressosTotal, Double preco)
    {
      var sessao = new Sessao(Guid.NewGuid(), filme, inicio, fim, quantidadeIngressosTotal, preco);
      return sessao;
    }

    public static Result<Sessao> CriarExistente(Guid id, Guid iDfilme, DateTime inicio, DateTime fim, int quantidadeIngressosTotal, Double preco, int quantidadeIngressosVendidos)
    {
      var sessao = new Sessao(id, iDfilme, inicio, fim, quantidadeIngressosTotal, preco, quantidadeIngressosVendidos);
      return sessao;
    }
  }
}

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
    public Guid Id { get; }
    public Filme _filme;
    public DateTime _inicio;
    public DateTime _fim;
    public int _quantidadeIngressosTotal;
    public double _preco;
    public int _quantidadeIngressosVendidos;
    private Sessao() { }
    public Sessao(Guid id, Filme filme, DateTime inicio, DateTime fim, int quantidadeIngressosTotal, double preco)
    {
      Id = id;
      _filme = filme;
      _inicio = inicio;
      _fim = fim;
      _quantidadeIngressosTotal = quantidadeIngressosTotal;
      _preco = preco;
      _quantidadeIngressosVendidos = 0;
    }
    public Sessao(Guid id, Filme filme, DateTime inicio, DateTime fim, int quantidadeIngressosTotal, double preco, int quantidadeIngressosVendidos)
    {
      Id = id;
      _filme = filme;
      _inicio = inicio;
      _fim = fim;
      _quantidadeIngressosTotal = quantidadeIngressosTotal;
      _preco = preco;
      _quantidadeIngressosVendidos = quantidadeIngressosVendidos;
    }
    public bool AceitaNovosIngressos()
    {
      return _quantidadeIngressosTotal >= (_quantidadeIngressosVendidos + 1);
    }

    public static Result<Sessao> Criar(Filme filme, DateTime inicio, DateTime fim, int quantidadeIngressosTotal, double preco)
    {
      var sessao = new Sessao(Guid.NewGuid(), filme, inicio, fim, quantidadeIngressosTotal, preco);
      return sessao;
    }

    public static Result<Sessao> CriarExistente(Filme filme, DateTime inicio, DateTime fim, int quantidadeIngressosTotal, double preco, int quantidadeIngressosVendidos)
    {
      var sessao = new Sessao(Guid.NewGuid(), filme, inicio, fim, quantidadeIngressosTotal, preco, quantidadeIngressosVendidos);
      return sessao;
    }
  }
}

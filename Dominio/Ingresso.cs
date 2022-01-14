using System;
using System.Collections;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace AplicacaoCinema.Dominio
{
  public sealed class Ingresso
  {
    public Guid Id { get; }
    public Sessao Sessao { get; }
    public DateTime Data { get; }
    private Ingresso() { }
    //Guid.NewGuid(), sessao.id, sessao.sala, sessao.DescricaoFilme, sessao.preco, data)
    private Ingresso(Guid id, Sessao sessao, DateTime data)
    {
      Id = id;
      Sessao = sessao;
      Data = data;
    }
    public static Result<List<Ingresso>> Criar(Sessao sessao, DateTime data, int quantidade)
    {
      List<Ingresso> ingressos = new List<Ingresso>();
      for (int i = 0; i < quantidade; i++)
      {
        if (!sessao.AceitaNovosIngressos())
          return Result.Failure<List<Ingresso>>("Sessao não aceita novas vendas");
        ingressos[i] = new Ingresso(Guid.NewGuid(), sessao, data);
      }

      return ingressos;
    }

  }

  public enum ESituacaoIngresso
  {
    Ativo,
    Cancelado,
    Usado
  }
}
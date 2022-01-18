using System;
using System.Collections;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace AplicacaoCinema.Dominio
{
  public sealed class Ingresso
  {
    public Guid Id { get; }
    public Guid SessaoId { get; }
    public DateTime Data { get; }
    private Ingresso() { }
    private Ingresso(Guid id, Guid sessaoId, DateTime data)
    {
      Id = id;
      SessaoId = sessaoId;
      Data = data;
    }
    public static Result<List<Ingresso>> Criar(Guid sessaoId, DateTime data, int quantidade)
    {
      List<Ingresso> ingressos = new List<Ingresso>();
      for (int i = 0; i < quantidade; i++)
      {
        ingressos.Add(new Ingresso(Guid.NewGuid(), sessaoId, data));
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
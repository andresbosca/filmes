using System;
using CSharpFunctionalExtensions;

namespace AplicacaoCinema.Dominio
{
  public sealed class Filme
  {
    private Filme() { }

    private Filme(Guid id, string titulo, int duracao, string sinopse)
    {
      Id = id;
      Titulo = titulo;
      Duracao = duracao;
      Sinopse = sinopse;
    }

    public Guid Id { get; }
    public string Titulo { get; }
    public int Duracao { get; }
    public string Sinopse { get; }

    public static Result<Filme> Criar(string titulo, int duracao, string sinopse)
    {
      if (string.IsNullOrEmpty(titulo))
        return Result.Failure<Filme>("Título deve ser preenchido");

      if (string.IsNullOrEmpty(duracao.ToString()))
        return Result.Failure<Filme>("Duração deve ser preenchido");

      if (string.IsNullOrEmpty(sinopse))
        return Result.Failure<Filme>("Sinopse deve ser preenchido");

      return new Filme(Guid.NewGuid(), titulo, duracao, sinopse);
    }
  }
}
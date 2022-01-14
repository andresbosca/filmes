using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplicacaoCinema.Hosting.Configuration;
using Microsoft.Extensions.Configuration;

namespace AplicacaoCinema.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ConfiguracoesExemploController : ControllerBase
  {
    private readonly IConfiguration _configuration;
    private readonly IngressosOptions _ingressosOptions;

    public ConfiguracoesExemploController(
        IConfiguration configuration,
        IngressosOptions ingressosOptions)
    {
      _configuration = configuration;
      _ingressosOptions = ingressosOptions;
    }

    [HttpGet]
    public IActionResult Recuperar()
    {
      var minhaChave = _configuration["MinhaChaveConfiguracao"];
      var usuarioFuncao = _configuration["Usuario:Funcao"];
      var usuarioNome = _configuration["Usuario:Nome"];

      return Ok(new
      {
        minhaChave,
        usuarioNome,
        usuarioFuncao
      });
    }

    [HttpGet("Ingressos")]
    public IActionResult RecuperarIngressos()
    {
      return Ok(_ingressosOptions);
    }
  }
}

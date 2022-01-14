using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AplicacaoCinema.Hosting.Atributos
{
  public class RequireHttpsOrCloseAttribute : RequireHttpsAttribute
  {
    protected override void HandleNonHttpsRequest(AuthorizationFilterContext filterContext)
    {
      filterContext.Result = new StatusCodeResult(400);
    }
  }
}

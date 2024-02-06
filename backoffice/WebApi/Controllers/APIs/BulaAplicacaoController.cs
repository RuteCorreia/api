using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs
{
    public class BulaAplicacaoController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace TodoWeb.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Index(int code)
        {
            if (code >= 400 && code <= 499)
                return View($"Errors/404");

            return View("Errors/Geral");
        }
    }
}

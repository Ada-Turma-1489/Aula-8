using Microsoft.AspNetCore.Mvc;

namespace DemoMvc.Controllers
{
    public class BatatinhaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

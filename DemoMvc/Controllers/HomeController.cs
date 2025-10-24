using Microsoft.AspNetCore.Mvc;

namespace DemoMvc.Controllers
{
    public class HomeController : Controller
    {
        //ROTEAMENTO: Mapeamento entre uma URL -> Action 
        //2 opções: 1 - por Atributos
        //          2 - por Convenção/Padrão/Patterns

        // Principal/Index
        public IActionResult Index()
        {
            // Convenção -> Regra que todo mundo/adota/CONHECE (nós, devs e o ASP.NET Core)
            // Views/Nome do Controller/Nome da Action

            return View();
        }

        // Principal/Cadastro
        public IActionResult Cadastro()
        {
            // Convenção -> Regra que todo mundo/adota/CONHECE (nós, devs e o ASP.NET Core)
            // Views/Nome do Controller/Nome da Action

            return View();
        }

        [Route("/exemplo")]
        public IActionResult Exemplo()
        {
            // Convenção -> Regra que todo mundo/adota/CONHECE (nós, devs e o ASP.NET Core)
            // Views/Nome do Controller/Nome da Action

            return View();
        }
    }
}

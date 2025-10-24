using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace TodoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5116/todo");

            var conteudo = await response.Content.ReadFromJsonAsync<IEnumerable<Todo>>();
            return View(conteudo);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace TodoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient client;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient(Program.HttpClientName);
        }

        // GET / ou GET /Home/Index
        public async Task<IActionResult> Index()
        {
            
            var response = await client.GetAsync("");

            var conteudo = await response.Content.ReadFromJsonAsync<IEnumerable<Todo>>();
            return View(conteudo);
        }

        // GET /Home/Details/5
        public async Task<IActionResult> Details(int id)
        {
            
            var response = await client.GetAsync($"{id}");

            if (response.IsSuccessStatusCode)
            {
                var todo = await response.Content.ReadFromJsonAsync<Todo>();
                return View(todo); // retorna a view Details
            }

            return Problem();
        }

        // GET /Home/Create -> retornar o HTML com o Formulário
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Store(Todo todo) 
        {
            
            var response = await client.PostAsJsonAsync("", todo);

            if (response.IsSuccessStatusCode)
            { 
                var responseTodo = await response.Content.ReadFromJsonAsync<Todo>();
                //return RedirectToAction(nameof(Details), new { id = responseTodo.Id });
                ViewBag.Success = true;
                ViewBag.TodoId = responseTodo.Id;
                ViewBag.Mensagem = "Tarefa criada com sucesso!";
            }
            else
            {
                ViewBag.Success = false;
                ViewBag.Mensagem = "Houve um erro na criação da tarefa.";
            }

            return View("Mensagem");
        }

        // GET /Home/Edit/5 -> Retornar um form HTML já preenchido com os detalhes da TODO que eu vou editar
        public async Task<IActionResult> Edit(int id)
        {
            
            var response = await client.GetAsync($"{id}");

            if (response.IsSuccessStatusCode)
            {
                var todo = await response.Content.ReadFromJsonAsync<Todo>();
                return View(todo);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Save(Todo todo)
        {
            
            var response = await client.PutAsJsonAsync($"{todo.Id}", todo);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Success = true;
                ViewBag.TodoId = todo.Id;
                ViewBag.Mensagem = "Tarefa atualizada com sucesso!";
            }
            else
            {
                ViewBag.Success = false;
                ViewBag.Mensagem = "Houve um erro na criação da tarefa.";
            }

            return View("Mensagem");
        }

        // GET /Home/Delete/123
        public async Task<IActionResult> Delete(int id)
        {
            
            var response = await client.GetAsync($"{id}");

            if (response.IsSuccessStatusCode)
            {
                var todo = await response.Content.ReadFromJsonAsync<Todo>();
                return View(todo);
            }

            return NotFound();
        }


        // POST /Home/Delete/123
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            
            var response = await client.DeleteAsync($"{id}");

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Success = true;
                ViewBag.Mensagem = "Tarefa apagada com sucesso!";
            }
            else
            {
                ViewBag.Success = false;
                ViewBag.Mensagem = "Houve um erro na remoção da tarefa.";
            }

            return View("Mensagem");
        }
    }
}

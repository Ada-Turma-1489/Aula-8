using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers
{
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext todoDbContext;

        public TodoController(TodoDbContext todoDbContext)
        {
            this.todoDbContext = todoDbContext;
        }

        [Route("/todo")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(todoDbContext.Todos);
        }

        [Route("/todo/{id:int}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var todo = todoDbContext.Todos.FirstOrDefault(x => x.Id == id);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [Route("/todo")]
        [HttpPost]
        public IActionResult Create([FromBody]Todo todo) // Biding
        {
            todoDbContext.Todos.Add(todo);
            todoDbContext.SaveChanges();
            
            return Created();
        }
    }
}

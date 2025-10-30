using Dominio;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace TodoApi.Controllers
{
    /// <summary>
    /// Controller that contains the actions to manipulate Todo objects
    /// </summary>
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class TodoController : ControllerBase
    {
        private readonly CaixaDbContext caixaDbContext;

        public TodoController(CaixaDbContext caixaDbContext)
        {
            this.caixaDbContext = caixaDbContext;
        }

        // No REST os Métodos/Verbos são Semânticos
        // CREATE   -> POST
        // READ     -> GET
        // UPDATE   -> PUT, PATCH
        // DELETE   -> DELETE
        // Abstração em .NET: Interface ou Classes Abstratas

        /// <summary>
        /// Get All Todos
        /// </summary>
        [Route("todo/")]
        [HttpGet]
        public IActionResult GetAll()
        {
            //var todos = caixaDbContext.Todos.Where(t => t.Tenant == tenantApi);
            return Ok(caixaDbContext.Todos);
        }

        /// <summary>
        /// Get a Todo object by it's Id
        /// </summary>
        /// <param name="id">The id of the Todo list I need to return</param>
        /// <response code="200">Returns the Todo when it's found</response>
        /// <response code="404">Todo was not found</response>
        [Route("todo/{id:int:min(0)}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Todo))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
        public IActionResult GetById(int id)
        {
            var todo = caixaDbContext.Todos.Find(id);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [Route("todo/")]
        [HttpPost]
        public IActionResult Create([FromBody] Todo todo)
        {
            caixaDbContext.Todos.Add(todo);
            caixaDbContext.SaveChanges();

            return CreatedAtAction("GetById", new { id = todo.Id }, todo);
        }

        [Route("todo/{id:int:min(0)}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var todo = caixaDbContext.Todos.Find(id);
            if (todo == null)
                return NotFound();

            caixaDbContext.Todos.Remove(todo);
            caixaDbContext.SaveChanges();

            return NoContent();
        }

        [Route("todo/{id:int:min(0)}")]
        [HttpPut]
        public IActionResult Update([FromBody] Todo todo, int id)
        {
            if (id != todo.Id)
                return BadRequest();

            var existing = caixaDbContext.Todos.Find(id);
            if (existing == null)
                return NotFound();

            existing.Description = todo.Description;
            existing.Priority = todo.Priority;

            caixaDbContext.Todos.Update(existing);
            caixaDbContext.SaveChanges();

            return NoContent();
        }
    }
}

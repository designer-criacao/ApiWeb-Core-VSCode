using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if(_context.TodoItems.Count() == 0)
            {
                //Criar uma nova TodoItem se a coleção for vázia,
                //o que significa que você não pode excluir todos os TodoItems
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        //GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        //GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if(todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
    }
}

//Inicio 30/12 - Pausa na parte de Roteamento e Caminhos de URL
//https://docs.microsoft.com/pt-br/aspnet/core/tutorials/first-web-api?view=aspnetcore-2.2&tabs=visual-studio-code
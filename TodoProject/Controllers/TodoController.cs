using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using TodoProject.Models;
using Microsoft.AspNetCore.Cors;

namespace TodoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly UserContext _dbContext;

        public TodoController(UserContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetTodos()
        {
            if (_dbContext.Todo == null)
            {
                return BadRequest();
            }
            return await _dbContext.Todo.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Todo>>> GetTodosById(int id)
        {
            if (_dbContext.Todo == null)
            {
                return BadRequest();
            }
            List<Todo> tempTodoList = new List<Todo>();
            foreach (var item in _dbContext.Todo)
            {
                if (id == item.userId)
                {
                    tempTodoList.Add(item);

                }
            }
            return Ok(tempTodoList);
        }
        [HttpPost("{id}")]
        public async Task<ActionResult<Todo>> CreateTodo(int id, string text)
        {
            var tempUser = _dbContext.Users.Find(id);
            if (tempUser == null)
            {
                return NotFound("User Not Found");
            }
            Todo tempTodo = new Todo
            {
                todoText = text,
                userId = tempUser.Id,
                isDone = false,
            };
            _dbContext.Todo.Add(tempTodo);

            await _dbContext.SaveChangesAsync();
            return Ok(tempTodo);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateTodo(int todoId, bool _boolean)
        {
            var updatedTodo = _dbContext.Todo.Find(todoId);
            if (updatedTodo == null)
                NotFound("User Not Found");
            updatedTodo.isDone = _boolean;
            _dbContext.Todo.Update(updatedTodo);
            await _dbContext.SaveChangesAsync();
            return Ok(updatedTodo);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteTodo(int todoId)
        {
            var deleteTodo = _dbContext.Todo.Find(todoId);
            if (deleteTodo == null)
            {
                return NotFound("User Not Found");
            }
            _dbContext.Todo.Remove(deleteTodo);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using TodoProject.Models;
using Microsoft.AspNetCore.Cors;
using TodoProject.Dtos;

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
        public async Task<ActionResult<List<Todo>>> GetTodosById(GetTodosByIdSet model)
        {
            if (_dbContext.Todo == null)
            {
                return BadRequest();
            }
            List<Todo> tempTodoList = new List<Todo>();
            foreach (var item in _dbContext.Todo)
            {
                if (model.id == item.userId)
                {
                    tempTodoList.Add(item);

                }
            }
            return Ok(tempTodoList);
        }
        [HttpPost("Post")]
        public async Task<ActionResult<Todo>> CreateTodo(CreateTodoSet model)
        {
            var tempUser = _dbContext.Users.Find(model.id);
            if (tempUser == null)
            {
                return NotFound("User Not Found");
            }
            Todo tempTodo = new Todo
            {
                todoText = model.text,
                userId = tempUser.Id,
                isDone = false,
            };
            _dbContext.Todo.Add(tempTodo);

            await _dbContext.SaveChangesAsync();
            return Ok(tempTodo);
        }
        [HttpPost("Update")]
        public async Task<ActionResult> UpdateTodo(UpdateTodoSet model)
        {
            var updatedTodo = _dbContext.Todo.Find(model.todoId);
            if (updatedTodo == null)
                NotFound("User Not Found"); 
            updatedTodo.isDone = model.boo;
            _dbContext.Todo.Update(updatedTodo);
            await _dbContext.SaveChangesAsync();
            return Ok(updatedTodo);
        }
        [HttpPost("Delete")]
        public async Task<ActionResult> DeleteTodo(DeleteTodoSet model)
        {
            var deleteTodo = _dbContext.Todo.Find(model.todoId);
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
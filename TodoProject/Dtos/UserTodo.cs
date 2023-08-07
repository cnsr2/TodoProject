using TodoProject.Models;

namespace TodoProject.Dtos
{
    public class UserTodo
    {
        public int userId { get; set; }
        public List<Todo> todoList { get; set; }
    }
}

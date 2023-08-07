namespace TodoProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }
    }
    public class Todo
    {
        public int todoId { get; set; }
        public string todoText { get; set; }
        public bool isActive { get; set; }

        public int userId { get; set; }
    }
}

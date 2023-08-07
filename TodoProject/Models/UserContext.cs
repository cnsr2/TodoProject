using Microsoft.EntityFrameworkCore;

namespace TodoProject.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todo { get; set; }

    }
}

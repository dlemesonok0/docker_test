using Microsoft.EntityFrameworkCore;

namespace TodoApi;

public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
{
    public DbSet<TodoItem> Todos => Set<TodoItem>();
}

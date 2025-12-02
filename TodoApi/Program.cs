using Microsoft.EntityFrameworkCore;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("TodoConnection") ?? "Data Source=todo.db";
builder.Services.AddDbContext<TodoContext>(options => options.UseSqlite(connectionString));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
    
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodoContext>();
    db.Database.EnsureCreated();
}

app.MapGet("/health", async () =>
    Results.Ok());

app.MapGet("/todos", async (TodoContext db) =>
    await db.Todos.AsNoTracking().ToListAsync());

app.MapGet("/todos/{id:int}", async (int id, TodoContext db) =>
    await db.Todos.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id)
        is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.MapPost("/todos", async (TodoItemCreateDto dto, TodoContext db) =>
{
    var todo = new TodoItem
    {
        Title = dto.Title,
        Notes = dto.Notes,
        IsComplete = dto.IsComplete
    };

    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todos/{todo.Id}", todo);
});

app.MapPut("/todos/{id:int}", async (int id, TodoItemUpdateDto dto, TodoContext db) =>
{
    var todo = await db.Todos.FirstOrDefaultAsync(t => t.Id == id);
    if (todo is null)
    {
        return Results.NotFound();
    }

    todo.Title = dto.Title;
    todo.Notes = dto.Notes;
    todo.IsComplete = dto.IsComplete;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todos/{id:int}", async (int id, TodoContext db) =>
{
    var todo = await db.Todos.FirstOrDefaultAsync(t => t.Id == id);
    if (todo is null)
    {
        return Results.NotFound();
    }

    db.Todos.Remove(todo);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

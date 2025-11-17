namespace TodoApi;

public class TodoItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Notes { get; set; }
    public bool IsComplete { get; set; }
}

public record TodoItemCreateDto(string Title, string? Notes, bool IsComplete);

public record TodoItemUpdateDto(string Title, string? Notes, bool IsComplete);

namespace TaskTrackerApp.UI.ViewModels;

public class TaskItemResponseViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public Guid UserId { get; set; }
}

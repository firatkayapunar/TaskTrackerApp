namespace TaskTrackerApp.UI.ViewModels;

public class UpdateTaskItemRequestViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
}

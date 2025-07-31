namespace TaskTrackerApp.UI.ViewModels;

public class CreateTaskItemRequestViewModel
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public Guid UserId { get; set; }
}

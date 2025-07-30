using TaskTrackerApp.Domain.Auditing.BaseClasses;
using TaskTrackerApp.Domain.Enums;

namespace TaskTrackerApp.Domain.Entities;

public class TaskItem : FullAuditableEntity
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; }

    public TaskItemState State { get; set; } = TaskItemState.NotStarted;

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}

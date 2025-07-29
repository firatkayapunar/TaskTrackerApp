using TaskTrackerApp.Domain.Common;
using TaskTrackerApp.Domain.Enums;
using TaskTrackerApp.Domain.Interfaces;

namespace TaskTrackerApp.Domain.Entities;

public class TaskItem : IEntity, IFullAuditable
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }

    public TaskState State{ get; set; } = TaskState.NotStarted;

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = "System";
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
}

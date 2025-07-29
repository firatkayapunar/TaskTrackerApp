using TaskTrackerApp.Domain.Common;
using TaskTrackerApp.Domain.Interfaces;

namespace TaskTrackerApp.Domain.Entities;

public class User : IEntity, IAuditable
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = "System";
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
}

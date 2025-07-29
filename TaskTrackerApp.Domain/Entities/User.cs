using TaskTrackerApp.Domain.Auditing.BaseClasses;

namespace TaskTrackerApp.Domain.Entities;

public class User : FullAuditableEntity
{
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}

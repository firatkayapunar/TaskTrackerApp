using TaskTrackerApp.Domain.Auditing.BaseClasses;

namespace TaskTrackerApp.Domain.Entities;

public class User : FullAuditableEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    public string FullName => $"{FirstName} {LastName}";

    public ICollection<TaskItem> Tasks { get; } = new List<TaskItem>();
}

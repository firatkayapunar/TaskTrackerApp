using TaskTrackerApp.Domain.Auditing.BaseClasses;
using TaskTrackerApp.Domain.Enums;

namespace TaskTrackerApp.Domain.Entities;

public class User : AuditableEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public UserRole Role { get; set; } = UserRole.User;
    public string FullName => $"{FirstName} {LastName}";
    public ICollection<TaskItem> Tasks { get; } = new List<TaskItem>();
}

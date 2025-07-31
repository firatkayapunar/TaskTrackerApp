using TaskTrackerApp.Domain.Auditing.Interfaces;
using TaskTrackerApp.Domain.Common;

namespace TaskTrackerApp.Domain.Auditing.BaseClasses;

public abstract class AuditableEntity : BaseEntity, IAuditable
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = "System";
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}


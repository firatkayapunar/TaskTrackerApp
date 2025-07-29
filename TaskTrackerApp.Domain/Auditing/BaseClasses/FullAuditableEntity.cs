using TaskTrackerApp.Domain.Auditing.Interfaces;

namespace TaskTrackerApp.Domain.Auditing.BaseClasses;

public abstract class FullAuditableEntity : AuditableEntity, IFullAuditable
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}

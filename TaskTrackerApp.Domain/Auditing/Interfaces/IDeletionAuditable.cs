namespace TaskTrackerApp.Domain.Auditing.Interfaces;

public interface IDeletionAuditable
{
    DateTime? DeletedAt { get; set; }
    string? DeletedBy { get; set; }
    bool IsDeleted { get; set; }
}

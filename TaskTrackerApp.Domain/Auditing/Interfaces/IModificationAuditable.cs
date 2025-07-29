namespace TaskTrackerApp.Domain.Auditing.Interfaces;

public interface IModificationAuditable
{
    DateTime? UpdatedAt { get; set; }
    string? UpdatedBy { get; set; }
}

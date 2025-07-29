namespace TaskTrackerApp.Domain.Auditing.Interfaces;

public interface ICreationAuditable
{
    DateTime CreatedAt { get; set; }
    string CreatedBy { get; set; }
}
 
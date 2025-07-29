namespace TaskTrackerApp.Domain.Common;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; set; }
}
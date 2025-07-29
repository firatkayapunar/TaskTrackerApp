namespace TaskTrackerApp.Domain.Interfaces;

public interface ICreationAuditable
{
    DateTime CreatedAt { get; set; }
    string CreatedBy { get; set; }
}

public interface IModificationAuditable
{
    DateTime? UpdatedAt { get; set; }
    string? UpdatedBy { get; set; }
}

public interface IDeletionAuditable
{
    DateTime? DeletedAt { get; set; }
    string? DeletedBy { get; set; }
    bool IsDeleted { get; set; }
}

public interface IAuditable : ICreationAuditable, IModificationAuditable { }

public interface IFullAuditable : IAuditable, IDeletionAuditable { }

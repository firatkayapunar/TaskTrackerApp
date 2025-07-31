namespace TaskTrackerApp.Application.CQRS.TaskItems.Queries.Response;

public sealed record GetTaskItemByIdQueryResponse(
    Guid Id,
    string Title,
    string? Description,
    DateTime? DueDate,
    bool IsCompleted,
    DateTime CreatedAt
);

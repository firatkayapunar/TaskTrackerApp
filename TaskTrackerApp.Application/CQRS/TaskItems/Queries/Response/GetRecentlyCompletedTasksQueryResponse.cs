namespace TaskTrackerApp.Application.CQRS.TaskItems.Queries.Response;

public sealed record GetRecentlyCompletedTasksQueryResponse(
    Guid Id,
    string Title,
    string? Description,
    DateTime? DueDate,
    DateTime CreatedAt
);
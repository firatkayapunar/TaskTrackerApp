namespace TaskTrackerApp.Application.CQRS.TaskItems.Queries.Response;

public sealed record GetRecentlyCompletedTasksQueryResponse(
    Guid Id,
    string Title,
    string? Description,
    string State,
    DateTime CreatedAt,
    DateTime? CompletedAt
);

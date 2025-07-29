namespace TaskTrackerApp.Application.CQRS.TaskItems.Queries.Response;

public sealed record GetAllTasksQueryResponse(
    Guid Id,
    string Title,
    string? Description,
    string State,
    DateTime CreatedAt
);

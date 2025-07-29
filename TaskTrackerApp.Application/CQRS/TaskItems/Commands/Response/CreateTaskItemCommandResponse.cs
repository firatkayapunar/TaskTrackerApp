namespace TaskTrackerApp.Application.CQRS.TaskItems.Commands.Response;

public sealed record CreateTaskItemCommandResponse(
    bool IsSuccess,
    Guid TaskItemId
);

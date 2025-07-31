using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Response;

namespace TaskTrackerApp.Application.CQRS.TaskItems.Commands.Request;

public sealed record UpdateTaskItemCommandRequest(
    Guid Id,
    string Title,
    string? Description,
    bool IsCompleted
) : IRequest<ServiceResult<UpdateTaskItemCommandResponse>>;

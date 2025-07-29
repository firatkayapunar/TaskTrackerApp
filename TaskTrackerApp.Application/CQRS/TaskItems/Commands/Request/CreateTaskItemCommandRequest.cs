using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Response;

namespace TaskTrackerApp.Application.CQRS.TaskItems.Commands.Request;

public sealed record CreateTaskItemCommandRequest(
    string Title,
    string? Description,
    DateTime DueDate,
    Guid UserId
) : IRequest<ServiceResult<CreateTaskItemCommandResponse>>;

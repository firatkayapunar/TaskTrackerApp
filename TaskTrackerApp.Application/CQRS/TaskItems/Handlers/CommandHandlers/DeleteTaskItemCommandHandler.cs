using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Request;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Response;
using TaskTrackerApp.Application.Repositories;

namespace TaskTrackerApp.Application.CQRS.TaskItems.Handlers.CommandHandlers;

public sealed class DeleteTaskItemCommandHandler
    : IRequestHandler<DeleteTaskItemCommandRequest, ServiceResult<DeleteTaskItemCommandResponse>>
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskItemCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<ServiceResult<DeleteTaskItemCommandResponse>> Handle(
        DeleteTaskItemCommandRequest request,
        CancellationToken cancellationToken)
    {
        var taskItem = await _taskRepository.GetByIdAsync(request.TaskItemId);

        if (taskItem is null)
            return ServiceResult<DeleteTaskItemCommandResponse>.Fail(ResultCode.NotFound, $"Task with ID '{request.TaskItemId}' not found.");

        _taskRepository.Delete(taskItem);

        var response = new DeleteTaskItemCommandResponse(true);

        return ServiceResult<DeleteTaskItemCommandResponse>.Success(response, ResultCode.NoContent);
    }
}

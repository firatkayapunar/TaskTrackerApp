using MediatR;
using TaskTrackerApp.Application.Common.Interfaces;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Request;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Response;
using TaskTrackerApp.Application.Repositories;

namespace TaskTrackerApp.Application.CQRS.TaskItems.Handlers.CommandHandlers;

public sealed class DeleteTaskItemCommandHandler(ITaskItemRepository taskRepository, IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteTaskItemCommandRequest, ServiceResult<DeleteTaskItemCommandResponse>>
{
    private readonly ITaskItemRepository _taskRepository = taskRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<DeleteTaskItemCommandResponse>> Handle(
        DeleteTaskItemCommandRequest request,
        CancellationToken cancellationToken)
    {
        var taskItem = await _taskRepository.GetByIdAsync(request.TaskItemId);

        if (taskItem is null)
            return ServiceResult<DeleteTaskItemCommandResponse>.Fail(ResultCode.NotFound, $"Task with ID '{request.TaskItemId}' not found.");

        _taskRepository.Delete(taskItem);
        await _unitOfWork.SaveChangeAsync();

        var response = new DeleteTaskItemCommandResponse(true);

        return ServiceResult<DeleteTaskItemCommandResponse>.Success(response, ResultCode.NoContent);
    }
}

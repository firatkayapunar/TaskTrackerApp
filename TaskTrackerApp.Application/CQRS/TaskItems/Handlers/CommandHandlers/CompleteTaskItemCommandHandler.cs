using MediatR;
using TaskTrackerApp.Application.Common.Interfaces;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Request;
using TaskTrackerApp.Application.Repositories;

public sealed class CompleteTaskItemCommandHandler(ITaskItemRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<CompleteTaskItemCommandRequest, ServiceResult>
{
    private readonly ITaskItemRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult> Handle(CompleteTaskItemCommandRequest request, CancellationToken cancellationToken)
    {
        var task = await _repository.GetByIdAsync(request.Id);

        if (task is null)
            return ServiceResult.Fail(ResultCode.NotFound, "Task not found.");

        if (task.IsCompleted)
            return ServiceResult.Fail(ResultCode.BadRequest, "Task is already completed.");

        task.IsCompleted = true;
        task.DueDate = DateTime.UtcNow;

        _repository.Update(task);
        await _unitOfWork.SaveChangeAsync();

        return ServiceResult.Success();
    }
}

using AutoMapper;
using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Request;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Response;
using TaskTrackerApp.Application.Repositories;

namespace TaskTrackerApp.Application.CQRS.TaskItems.Handlers.CommandHandlers;

public sealed class UpdateTaskItemCommandHandler : IRequestHandler<UpdateTaskItemCommandRequest, ServiceResult<UpdateTaskItemCommandResponse>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public UpdateTaskItemCommandHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResult<UpdateTaskItemCommandResponse>> Handle(UpdateTaskItemCommandRequest request, CancellationToken cancellationToken)
    {
        var taskItem = await _taskRepository.GetByIdAsync(request.Id);

        if (taskItem is null)
            return ServiceResult<UpdateTaskItemCommandResponse>.Fail(ResultCode.NotFound, $"Task with ID '{request.Id}' was not found.");

        var isDuplicate = await _taskRepository.AnyAsync(t => t.Id != request.Id && t.Title == request.Title && t.UserId == taskItem.UserId);
        if (isDuplicate)
            return ServiceResult<UpdateTaskItemCommandResponse>.Fail(ResultCode.BadRequest, $"A task titled '{request.Title}' already exists.");

        _mapper.Map(request, taskItem);

        _taskRepository.Update(taskItem);

        return ServiceResult<UpdateTaskItemCommandResponse>.Success(new UpdateTaskItemCommandResponse(true));
    }
}

using AutoMapper;
using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Request;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Response;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.Domain.Entities;

namespace TaskTrackerApp.Application.CQRS.TaskItems.Handlers.CommandHandlers;

public sealed class CreateTaskItemCommandHandler : IRequestHandler<CreateTaskItemCommandRequest, ServiceResult<CreateTaskItemCommandResponse>>
{
    private readonly ITaskItemRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateTaskItemCommandHandler(ITaskItemRepository taskRepository, IUserRepository userRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResult<CreateTaskItemCommandResponse>> Handle(CreateTaskItemCommandRequest request, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.AnyAsync(u => u.Id == request.UserId);

        if (!userExists)
            return ServiceResult<CreateTaskItemCommandResponse>.Fail(ResultCode.BadRequest, $"User with ID '{request.UserId}' does not exist.");

        var duplicate = await _taskRepository.AnyAsync(t => t.Title == request.Title && t.UserId == request.UserId);

        if (duplicate)
            return ServiceResult<CreateTaskItemCommandResponse>.Fail(ResultCode.BadRequest, $"A task titled '{request.Title}' already exists for this user.");

        var taskItem = _mapper.Map<TaskItem>(request);

        await _taskRepository.AddAsync(taskItem);

        var response = new CreateTaskItemCommandResponse(true, taskItem.Id);

        return ServiceResult<CreateTaskItemCommandResponse>.Success(response, ResultCode.Created);
    }
}

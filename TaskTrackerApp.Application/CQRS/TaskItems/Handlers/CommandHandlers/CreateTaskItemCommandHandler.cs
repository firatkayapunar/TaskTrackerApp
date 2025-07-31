using AutoMapper;
using MediatR;
using TaskTrackerApp.Application.Common.Interfaces;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Request;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Response;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.Domain.Entities;

namespace TaskTrackerApp.Application.CQRS.TaskItems.Handlers.CommandHandlers;

public sealed class CreateTaskItemCommandHandler(ITaskItemRepository taskRepository, IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CreateTaskItemCommandRequest, ServiceResult<CreateTaskItemCommandResponse>>
{
    private readonly ITaskItemRepository _taskRepository = taskRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

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
        await _unitOfWork.SaveChangeAsync();

        var response = new CreateTaskItemCommandResponse(true, taskItem.Id);

        return ServiceResult<CreateTaskItemCommandResponse>.Success(response, ResultCode.Created);
    }
}

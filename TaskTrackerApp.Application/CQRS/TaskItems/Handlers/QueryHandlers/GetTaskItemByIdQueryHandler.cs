using AutoMapper;
using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.CQRS.TaskItems.Queries.Request;
using TaskTrackerApp.Application.CQRS.TaskItems.Queries.Response;
using TaskTrackerApp.Application.Repositories;

namespace TaskTrackerApp.Application.CQRS.TaskItems.Handlers.QueryHandlers;

public sealed class GetTaskItemByIdQueryHandler
    : IRequestHandler<GetTaskItemByIdQueryRequest, ServiceResult<GetTaskItemByIdQueryResponse>>
{
    private readonly ITaskItemRepository _taskRepository;
    private readonly IMapper _mapper;

    public GetTaskItemByIdQueryHandler(ITaskItemRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResult<GetTaskItemByIdQueryResponse>> Handle(
        GetTaskItemByIdQueryRequest request,
        CancellationToken cancellationToken)
    {
        var taskItem = await _taskRepository.GetByIdAsync(request.TaskItemId);

        if (taskItem is null)
        {
            return ServiceResult<GetTaskItemByIdQueryResponse>.Fail(
                ResultCode.NotFound,
                $"Task with ID '{request.TaskItemId}' not found."
            );
        }

        var response = _mapper.Map<GetTaskItemByIdQueryResponse>(taskItem);

        return ServiceResult<GetTaskItemByIdQueryResponse>.Success(response);
    }
}

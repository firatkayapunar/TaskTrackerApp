using AutoMapper;
using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.CQRS.TaskItems.Queries.Request;
using TaskTrackerApp.Application.CQRS.TaskItems.Queries.Response;
using TaskTrackerApp.Application.Repositories;

namespace TaskTrackerApp.Application.CQRS.TaskItems.Handlers.QueryHandlers;

public sealed class GetRecentlyCompletedTasksQueryHandler(ITaskItemRepository taskRepository, IMapper mapper)
        : IRequestHandler<GetRecentlyCompletedTasksQueryRequest, ServiceResult<List<GetRecentlyCompletedTasksQueryResponse>>>
{
    private readonly ITaskItemRepository _taskRepository = taskRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ServiceResult<List<GetRecentlyCompletedTasksQueryResponse>>> Handle(
        GetRecentlyCompletedTasksQueryRequest request,
        CancellationToken cancellationToken)
    {
        var taskItems = await _taskRepository.GetRecentlyCompletedTasksAsync();

        var response = _mapper.Map<List<GetRecentlyCompletedTasksQueryResponse>>(taskItems);

        return ServiceResult<List<GetRecentlyCompletedTasksQueryResponse>>.Success(response);
    }
}

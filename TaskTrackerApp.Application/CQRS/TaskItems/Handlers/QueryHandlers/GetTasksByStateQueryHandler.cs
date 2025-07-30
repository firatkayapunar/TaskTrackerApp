using AutoMapper;
using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.CQRS.TaskItems.Queries.Request;
using TaskTrackerApp.Application.CQRS.TaskItems.Queries.Response;
using TaskTrackerApp.Application.Repositories;

namespace TaskTrackerApp.Application.CQRS.TaskItems.Handlers.QueryHandlers;

public sealed class GetTasksByStateQueryHandler
    : IRequestHandler<GetTasksByStateQueryRequest, ServiceResult<List<GetTasksByStateQueryResponse>>>
{
    private readonly ITaskItemRepository _taskRepository;
    private readonly IMapper _mapper;

    public GetTasksByStateQueryHandler(ITaskItemRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<GetTasksByStateQueryResponse>>> Handle(
        GetTasksByStateQueryRequest request,
        CancellationToken cancellationToken)
    {
        var taskItems = await _taskRepository.GetTasksByStateAsync(request.State);

        var response = _mapper.Map<List<GetTasksByStateQueryResponse>>(taskItems);

        return ServiceResult<List<GetTasksByStateQueryResponse>>.Success(response);
    }
}

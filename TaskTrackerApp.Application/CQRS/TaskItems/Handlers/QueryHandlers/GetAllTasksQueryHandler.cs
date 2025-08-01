﻿using AutoMapper;
using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.CQRS.TaskItems.Queries.Request;
using TaskTrackerApp.Application.CQRS.TaskItems.Queries.Response;
using TaskTrackerApp.Application.Repositories;

namespace TaskTrackerApp.Application.CQRS.TaskItems.Handlers.QueryHandlers;

public sealed class GetAllTasksQueryHandler(ITaskItemRepository taskRepository, IMapper mapper)
        : IRequestHandler<GetAllTasksQueryRequest, ServiceResult<List<GetAllTasksQueryResponse>>>
{
    private readonly ITaskItemRepository _taskRepository = taskRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ServiceResult<List<GetAllTasksQueryResponse>>> Handle(
        GetAllTasksQueryRequest request,
        CancellationToken cancellationToken)
    {
        var taskItems = await _taskRepository.GetAllTasksAsync();

        var response = _mapper.Map<List<GetAllTasksQueryResponse>>(taskItems);

        return ServiceResult<List<GetAllTasksQueryResponse>>.Success(response);
    }
}

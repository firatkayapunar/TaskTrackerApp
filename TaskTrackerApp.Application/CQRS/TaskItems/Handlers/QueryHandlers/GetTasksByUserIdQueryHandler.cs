﻿using AutoMapper;
using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.CQRS.TaskItems.Queries.Request;
using TaskTrackerApp.Application.CQRS.TaskItems.Queries.Response;
using TaskTrackerApp.Application.Repositories;

namespace TaskTrackerApp.Application.CQRS.TaskItems.Handlers.QueryHandlers;

public sealed class GetTasksByUserIdQueryHandler(ITaskItemRepository taskRepository, IMapper mapper)
        : IRequestHandler<GetTasksByUserIdQueryRequest, ServiceResult<List<GetTasksByUserIdQueryResponse>>>
{
    private readonly ITaskItemRepository _taskRepository = taskRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ServiceResult<List<GetTasksByUserIdQueryResponse>>> Handle(
        GetTasksByUserIdQueryRequest request,
        CancellationToken cancellationToken)
    {
        var taskItems = await _taskRepository.GetTasksByUserIdAsync(request.UserId);

        var response = _mapper.Map<List<GetTasksByUserIdQueryResponse>>(taskItems);

        return ServiceResult<List<GetTasksByUserIdQueryResponse>>.Success(response);
    }
}

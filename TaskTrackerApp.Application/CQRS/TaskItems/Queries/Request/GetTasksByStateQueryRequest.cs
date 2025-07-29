using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.CQRS.TaskItems.Queries.Response;
using TaskTrackerApp.Domain.Enums;

namespace TaskTrackerApp.Application.CQRS.TaskItems.Queries.Request;

public sealed record GetTasksByStateQueryRequest(TaskState State)
    : IRequest<ServiceResult<List<GetTasksByStateQueryResponse>>>;

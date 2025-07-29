using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.Application.CQRS.TaskItems.Queries.Response;

namespace TaskTrackerApp.Application.CQRS.TaskItems.Queries.Request;

public sealed record GetRecentlyCompletedTasksQueryRequest
    : IRequest<ServiceResult<List<GetRecentlyCompletedTasksQueryResponse>>>;

using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.CQRS.Users.Queries.Response;

namespace TaskTrackerApp.CQRS.Users.Queries.Request;

public sealed record GetUserByIdQueryRequest(Guid Id)
    : IRequest<ServiceResult<GetUserByIdQueryResponse>>;

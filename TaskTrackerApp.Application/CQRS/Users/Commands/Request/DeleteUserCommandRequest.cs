using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.CQRS.Users.Commands.Response;

namespace TaskTrackerApp.CQRS.Users.Commands.Request;

public sealed record DeleteUserCommandRequest(Guid Id)
    : IRequest<ServiceResult<DeleteUserCommandResponse>>;

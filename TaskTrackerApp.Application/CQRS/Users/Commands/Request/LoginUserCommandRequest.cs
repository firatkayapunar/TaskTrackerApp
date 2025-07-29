using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.CQRS.Users.Commands.Response;

namespace TaskTrackerApp.CQRS.Users.Commands.Request;

public sealed record LoginUserCommandRequest(
    string Email,
    string Password
) : IRequest<ServiceResult<LoginUserCommandResponse>>;

using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.CQRS.Users.Commands.Response;

namespace TaskTrackerApp.CQRS.Users.Commands.Request;

public sealed record CreateUserCommandRequest(
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string Password
) : IRequest<ServiceResult<CreateUserCommandResponse>>;

using MediatR;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.CQRS.Users.Commands.Response;
using TaskTrackerApp.Domain.Enums;

namespace TaskTrackerApp.CQRS.Users.Commands.Request;

public sealed record UpdateUserCommandRequest(
    Guid Id,
    string FirstName,
    string LastName,
    string Username,
    string Email,
    UserRole Role
) : IRequest<ServiceResult<UpdateUserCommandResponse>>;

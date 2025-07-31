using TaskTrackerApp.Domain.Enums;

namespace TaskTrackerApp.CQRS.Users.Commands.Response;

public sealed record LoginUserCommandResponse(
    string Token,
    string Email,
    string Username,
    UserRole Role,
    Guid UserId
);

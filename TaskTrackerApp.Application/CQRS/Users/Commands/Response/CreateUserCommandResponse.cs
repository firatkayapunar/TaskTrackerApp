using TaskTrackerApp.Domain.Enums;

namespace TaskTrackerApp.CQRS.Users.Commands.Response;

public sealed record CreateUserCommandResponse(
    Guid Id,
    string Email,
    string FullName,
    UserRole Role 
);

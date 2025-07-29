namespace TaskTrackerApp.CQRS.Users.Commands.Response;

public sealed record UpdateUserCommandResponse(
    Guid Id,
    string Username,
    string Email,
    string FullName
);

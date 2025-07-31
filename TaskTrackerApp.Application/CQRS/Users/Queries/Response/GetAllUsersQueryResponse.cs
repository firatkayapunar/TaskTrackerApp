using TaskTrackerApp.Domain.Enums;

namespace TaskTrackerApp.CQRS.Users.Queries.Response;

public sealed record GetAllUsersQueryResponse(
    Guid Id,
    string Username,
    string Email,
    string FullName,
    UserRole Role
);

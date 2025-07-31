using TaskTrackerApp.Domain.Enums;

namespace TaskTrackerApp.CQRS.Users.Queries.Response;

public sealed record GetUserByIdQueryResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string FullName,
    UserRole UserRole
);

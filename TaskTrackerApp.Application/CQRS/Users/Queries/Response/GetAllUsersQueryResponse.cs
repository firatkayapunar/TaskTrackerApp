namespace TaskTrackerApp.CQRS.Users.Queries.Response;

public sealed record GetAllUsersQueryResponse(
    Guid Id,
    string Username,
    string Email,
    string FullName
);

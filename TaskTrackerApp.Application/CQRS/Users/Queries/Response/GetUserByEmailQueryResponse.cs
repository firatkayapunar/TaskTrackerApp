namespace TaskTrackerApp.CQRS.Users.Queries.Response;

public sealed record GetUserByEmailQueryResponse(
    Guid Id,
    string Username,
    string Email,
    string FullName
);

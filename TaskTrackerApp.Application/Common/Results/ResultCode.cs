namespace TaskTrackerApp.Application.Common.Results;

public enum ResultCode
{
    Success = 0,         // 200 OK
    Created = 1,         // 201 Created
    NoContent = 2,       // 204 NoContent
    BadRequest = 3,      // 400 Bad Request
    Unauthorized = 4,      // 401 Bad Unauthorized
    NotFound = 5,        // 404 Not Found
    Conflict = 6,        // 409 Conflict
    InternalError = 7    // 500 Internal Server Error
}

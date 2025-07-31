namespace TaskTrackerApp.UI.Constants;

public static class ApiRoutes
{
    public const string Login = "api/users/login";
    public const string Register = "api/users/register";
    public const string GetTasksByUserId = "api/taskitems/by-user";
    public const string DeleteTask = "api/taskitems";
    public const string TaskItemCreate = "api/taskitems";
    public const string TaskItemById = "api/taskitems";
    public const string TaskItemUpdate = "api/taskitems";
    public const string CompleteTask = "api/taskitems/complete";
}

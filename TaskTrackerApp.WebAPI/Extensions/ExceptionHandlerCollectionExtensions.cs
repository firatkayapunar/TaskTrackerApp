using TaskTrackerApp.WebAPI.ExceptionHandlers.Handlers;

namespace TaskTrackerApp.WebAPI.Extensions;

public static class ExceptionHandlerCollectionExtensions
{
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<SqlExceptionHandler>();
        services.AddExceptionHandler<TimeoutExceptionHandler>();
        services.AddExceptionHandler<UnhandledExceptionHandler>();

        return services;
    }
}

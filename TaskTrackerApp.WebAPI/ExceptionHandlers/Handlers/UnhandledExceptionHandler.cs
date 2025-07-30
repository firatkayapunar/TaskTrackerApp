using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.WebAPI.ExceptionHandlers.Base;

namespace TaskTrackerApp.WebAPI.ExceptionHandlers.Handlers;

public sealed class UnhandledExceptionHandler : BaseExceptionHandler<Exception>
{
    protected override ServiceResult HandleException(Exception exception)
    {
        return ServiceResult.Fail(ResultCode.InternalError, "Something went wrong on our end. Please try again later.");
    }
}

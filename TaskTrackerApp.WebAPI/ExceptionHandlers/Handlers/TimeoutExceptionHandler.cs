using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.WebAPI.ExceptionHandlers.Base;

namespace TaskTrackerApp.WebAPI.ExceptionHandlers.Handlers;

public sealed class TimeoutExceptionHandler : BaseExceptionHandler<TimeoutException>
{
    protected override ServiceResult HandleException(TimeoutException exception)
    {
        return ServiceResult.Fail(ResultCode.InternalError, "The operation took too long to complete and timed out.");
    }
}

using Microsoft.AspNetCore.Diagnostics;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.WebAPI.Extensions;

namespace TaskTrackerApp.WebAPI.ExceptionHandlers.Base;

public abstract class BaseExceptionHandler<TException> : IExceptionHandler where TException : Exception
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
      
        if (exception is not TException typedException)
            return false;

        var result = HandleException(typedException);

        context.Response.StatusCode = (int)result.ResultCode.ToHttpStatusCode();
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(result, cancellationToken);

        return true;
    }

    protected abstract ServiceResult HandleException(TException exception);
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.WebAPI.Extensions;

namespace TaskTrackerApp.WebAPI.Filters;

public class FluentValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var validationErrors = context.ModelState
                .Where(entry => entry.Value!.Errors.Count > 0)
                .SelectMany(entry => entry.Value!.Errors)
                .Select(error => error.ErrorMessage)
                .Where(message => !string.IsNullOrWhiteSpace(message))
                .ToList();

            var serviceResult = ServiceResult.Fail(ResultCode.BadRequest, validationErrors);

            context.Result = new ObjectResult(serviceResult)
            {
                StatusCode = (int)serviceResult.ResultCode.ToHttpStatusCode()
            };

            return;
        }

        await next();
    }
}

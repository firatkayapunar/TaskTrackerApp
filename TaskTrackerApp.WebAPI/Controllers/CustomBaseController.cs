using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.WebAPI.Extensions;

namespace TaskTrackerApp.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public abstract class CustomBaseController : ControllerBase
{
    [NonAction]
    protected IActionResult CreateActionResult(ServiceResult result)
    {
        if (result.IsSuccess)
            return StatusCode((int)GetStatusCode(result));

        return CreateErrorResponse(result);
    }

    [NonAction]
    protected IActionResult CreateActionResult<T>(ServiceResult<T> result)
    {
        if (result.IsSuccess)
            return StatusCode((int)GetStatusCode(result), result.Data);

        return CreateErrorResponse(result);
    }

    [NonAction]
    protected IActionResult CreateActionResult<T>(ServiceResult<T> result, string locationUrl)
    {
        if (!result.IsSuccess)
            return CreateErrorResponse(result);

        var statusCode = GetStatusCode(result);

        if (statusCode == HttpStatusCode.Created && !string.IsNullOrWhiteSpace(locationUrl))
            Response.Headers.Location = locationUrl;

        return StatusCode((int)statusCode, result.Data);
    }

    [NonAction]
    private IActionResult CreateErrorResponse(BaseServiceResult result)
        => StatusCode((int)GetStatusCode(result), result.Errors);

    [NonAction]
    private HttpStatusCode GetStatusCode(BaseServiceResult result)
        => result.ResultCode.ToHttpStatusCode();
}

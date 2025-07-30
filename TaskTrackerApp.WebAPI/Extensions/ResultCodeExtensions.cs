using System.Net;
using TaskTrackerApp.Application.Common.Results;

namespace TaskTrackerApp.WebAPI.Extensions;

public static class ResultCodeExtensions
{
    public static HttpStatusCode ToHttpStatusCode(this ResultCode code) => code switch
    {
        ResultCode.Success => HttpStatusCode.OK,                  // 200
        ResultCode.Created => HttpStatusCode.Created,             // 201
        ResultCode.NoContent => HttpStatusCode.NoContent,         // 204
        ResultCode.BadRequest => HttpStatusCode.BadRequest,       // 400
        ResultCode.Unauthorized => HttpStatusCode.Unauthorized,   // 401
        ResultCode.NotFound => HttpStatusCode.NotFound,           // 404
        ResultCode.Conflict => HttpStatusCode.Conflict,           // 409
        ResultCode.InternalError => HttpStatusCode.InternalServerError, // 500
        _ => HttpStatusCode.InternalServerError
    };
}

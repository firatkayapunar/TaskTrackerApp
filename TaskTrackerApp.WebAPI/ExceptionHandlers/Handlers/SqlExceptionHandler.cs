using Microsoft.Data.SqlClient;
using TaskTrackerApp.Application.Common.Results;
using TaskTrackerApp.WebAPI.ExceptionHandlers.Base;

namespace TaskTrackerApp.WebAPI.ExceptionHandlers.Handlers;

public sealed class SqlExceptionHandler : BaseExceptionHandler<SqlException>
{
    protected override ServiceResult HandleException(SqlException exception)
    {
        return ServiceResult.Fail(ResultCode.InternalError, "An error occurred during the database operation.");
    }
}

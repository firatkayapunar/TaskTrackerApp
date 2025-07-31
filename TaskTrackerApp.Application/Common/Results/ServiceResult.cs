using System.Text.Json.Serialization;

namespace TaskTrackerApp.Application.Common.Results;

public abstract class BaseServiceResult
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<string> Errors { get; init; } = new();
    [JsonIgnore]
    public ResultCode ResultCode { get; init; }
    [JsonIgnore]
    public bool IsSuccess => ResultCode == ResultCode.Success;
}

public class ServiceResult : BaseServiceResult
{
    private ServiceResult() { }

    public static ServiceResult Success(ResultCode resultCode = ResultCode.Success) => new() { ResultCode = resultCode };

    public static ServiceResult Fail(ResultCode resultCode, params string[] errors) => new() { ResultCode = resultCode, Errors = errors.Where(error => !string.IsNullOrWhiteSpace(error)).ToList() };

    public static ServiceResult Fail(ResultCode resultCode, List<string> errors) => new() { ResultCode = resultCode, Errors = errors.Where(error => !string.IsNullOrWhiteSpace(error)).ToList() };
}
public class ServiceResult<T> : BaseServiceResult
{
    public T? Data { get; init; }

    private ServiceResult() { }

    public static ServiceResult<T> Success(T data, ResultCode resultCode = ResultCode.Success) => new() { Data = data, ResultCode = resultCode };

    public static ServiceResult<T> Fail(ResultCode resultCode, params string[] errors) => new() { ResultCode = resultCode, Errors = errors.Where(error => !string.IsNullOrWhiteSpace(error)).ToList() };

    public static ServiceResult<T> Fail(List<string> errors, ResultCode resultCode) => new() { ResultCode = resultCode, Errors = errors.Where(error => !string.IsNullOrWhiteSpace(error)).ToList() };
}

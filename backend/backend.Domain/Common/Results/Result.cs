namespace backend.Domain.Common.Results;
// Might add enum ErrorType

public class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public string? Error { get; }
    public ErrorType? ErrorType{ get; }

    protected Result(bool isSuccess, T? value, string? error, ErrorType? errorType)
    {
        switch (isSuccess)
        {
            case true when error is not null:
                throw new InvalidOperationException("Success Result cannot be Error.");
            case false when error is null:
                throw new InvalidOperationException("Failure Result must contain Error.");
        }

        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        ErrorType = errorType;
    }

    public static Result<T> Success(T value) => new(true, value, null, null);
    public static Result<T> Failure(string error, ErrorType errorType = Results.ErrorType.Generic)
        => new(false, default, error, errorType);
}

public sealed class Result : Result<bool>
{
    private Result(bool isSuccess, string? error, ErrorType? errorType) : base(isSuccess, isSuccess, error, errorType) { }
    public static Result Success() => new(true, null, null);
    public static new Result Failure(string error, ErrorType errorType = Results.ErrorType.Generic) => new(false, error, errorType);
}
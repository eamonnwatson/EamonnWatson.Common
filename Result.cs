namespace EamonnWatson.Common;
public class Result
{
    public bool IsSuccess => Errors.Count == 0;
    public bool IsFailure => !IsSuccess;
    public List<Error> Errors { get; } = [];
    internal Result() { }
    public static Result Ok() => new();
    public static Result<TData> Ok<TData>(TData value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));
        return new Result<TData>(value);
    }
    public static Result Fail(string message) => new Result().WithError(message);
    public static Result Fail(Error error) => new Result().WithError(error);
    public static Result Fail(IEnumerable<Error> errors) => new Result().WithErrors(errors);
    public static Result<TData> Fail<TData>(string message) => new Result<TData>(default).WithError(message);
    public static Result<TData> Fail<TData>(Error error) => new Result<TData>(default).WithError(error);
    public static Result<TData> Fail<TData>(IEnumerable<Error> errors) => new Result<TData>(default).WithErrors(errors);

    public Result<TNewValue> Bind<TNewValue>(Func<Result<TNewValue>> bindFunction)
    {
        if (IsSuccess)
            return bindFunction();

        return new Result<TNewValue>(default).WithErrors(Errors);
    }
}

public class Result<TData> : Result
{
    public TData? Value { get; }
    internal Result(TData? value) { Value = value; }

    public static implicit operator Result<TData>(TData value) => Ok(value);
}

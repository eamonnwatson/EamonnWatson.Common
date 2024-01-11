namespace EamonnWatson.Common;
public class Result
{
    public bool IsSuccess => Errors.Count == 0;
    public bool IsFailure => !IsSuccess;
    public List<Error> Errors { get; } = [];
    protected Result() { }
    public static Result Ok() => new();
    public static Result Fail(Error error) => new Result().WithError(error);
    public static Result Fail(string message) => Fail(new Error(message));
    public static Result Fail(IEnumerable<Error> errors) => new Result().WithErrors(errors);
}

public class Result<TData> : Result
{
    public TData Value { get; }
    private Result(TData value)
    {
        Value = value;
    }
    public static Result<TData> Ok(TData value) => new(value);
    public static Result<TData> Fail(TData value, Error error) => new Result<TData>(value).WithError(error);
    public static Result<TData> Fail(TData value, string message) => Fail(value, new Error(message));
    public static Result<TData> Fail(TData value, IEnumerable<Error> errors) => new Result<TData>(value).WithErrors(errors);

    public static implicit operator Result<TData>(TData value) => Ok(value);
}

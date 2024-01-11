namespace EamonnWatson.Common;
public static class ResultExtensions
{
    public static TResult WithError<TResult>(this TResult result, string message) where TResult : Result
    {
        result.Errors.Add(new Error(message));
        return result;
    }
    public static TResult WithErrors<TResult>(this TResult result, IEnumerable<Error> errors) where TResult : Result
    {
        result.Errors.AddRange(errors);
        return result;
    }

    public static TResult WithError<TResult>(this TResult result, Error error) where TResult : Result
    {
        result.Errors.Add(error);
        return result;
    }
}

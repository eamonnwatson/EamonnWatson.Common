using System.Runtime;

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

    public static Result<TData> FailIf<TData>(this Result<TData> result, Func<TData, bool> predicate, Error error)
    {
        if (result.IsFailure)
            return result;

        return predicate(result.Value!) 
            ? Result.Fail<TData>(error) 
            : result;
    }

    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> mappingFunction)
    {
        return result.IsSuccess 
            ? Result.Ok(mappingFunction(result.Value!)) 
            : Result.Fail<TOut>(result.Errors);
    }

    public static void Match<TResult>(this TResult result, Action<TResult> onSuccess, Action<TResult> onFailure) where TResult : Result
    {
        ArgumentNullException.ThrowIfNull(onSuccess, nameof(onSuccess));
        ArgumentNullException.ThrowIfNull(onFailure, nameof(onFailure));

        if (result.IsFailure)
        {
            onFailure(result);
            return;
        }

        onSuccess(result);
    }

    public static TOut Match<TResult, TOut>(this TResult result, Func<TResult, TOut> onSuccess, Func<TResult, TOut> onFailure) where TResult : Result
    {
        ArgumentNullException.ThrowIfNull(onSuccess, nameof(onSuccess));
        ArgumentNullException.ThrowIfNull(onFailure, nameof(onFailure));

        if (result.IsFailure)
        {
            return onFailure(result);
        }

        return onSuccess(result);
    }
}

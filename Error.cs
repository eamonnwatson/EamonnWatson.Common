namespace EamonnWatson.Common;
public record Error(string Message, string Code, string Source)
{
    public Exception? Exception { get; }
    public Error(string message) : this(message, string.Empty, string.Empty) { }
    public Error(string message, string code) : this(message, code, string.Empty) { }
    public Error(Exception exception) : this(exception.Message, string.Empty, exception.Source ?? string.Empty)
    {
        Exception = exception;
    }
    public Error(Exception exception, string message) : this(message, string.Empty, exception.Source ?? string.Empty)
    {
        Exception = exception;
    }
}

namespace Thread.Domain.Helpers;
public readonly struct Result<TValue, TError>
{
    private readonly bool _success;
    public readonly TValue Value;
    public readonly TError Error;

    private Result(TValue v, TError e, bool success)
    {
        Value = v;
        Error = e;
        _success = success;
    }

    public bool IsSuccess => _success;

    public static Result<TValue, TError> Ok(TValue v)
    {
        return new(v, default, true);
    }

    public static Result<TValue, TError> Err(TError e)
    {
        return new(default, e, false);
    }

    public static implicit operator Result<TValue, TError>(TValue v) => new(v, default, true);
    public static implicit operator Result<TValue, TError>(TError e) => new(default, e, false);

    public TResult Match<TResult>(
            Func<TValue, TResult> success,
            Func<TError, TResult> failure) =>
        _success ? success(Value) : failure(Error);

    public T Match<T>(Func<T, T> value)
    {
        throw new NotImplementedException();
    }
}
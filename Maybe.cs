using System.Diagnostics.CodeAnalysis;

namespace EamonnWatson.Common;
public readonly partial struct Maybe<T> : IEquatable<Maybe<T>>, IEquatable<object>, IMaybe<T>
{
    private readonly bool _isValueSet;

    private readonly T? _value;

    public bool HasNoValue => !HasValue;
    public bool HasValue => _isValueSet;
    public static Maybe<T> None => new();
    public T Value => GetValueOrThrow();

    private Maybe(T? value)
    {
        if (value == null)
        {
            _isValueSet = false;
            _value = default;
            return;
        }

        _isValueSet = true;
        _value = value;
    }

    public static implicit operator Maybe<T>(T? value)
    {
        if (value is Maybe<T> m)
        {
            return m;
        }

        return Maybe.From(value);
    }

    public static implicit operator Maybe<T>(Maybe _) => None;

    public static bool operator ==(Maybe<T> maybe, T? value)
    {
        if (value is Maybe<T>)
            return maybe.Equals(value);

        if (maybe.HasNoValue)
            return value is null;

        if (maybe._value is null && value is null)
            return true;

        if (maybe._value is null || value is null)
            return false;

        return maybe._value.Equals(value);
    }

    public static bool operator !=(Maybe<T> maybe, T? value)
    {
        return !(maybe == value);
    }

    public static bool operator ==(Maybe<T> maybe, object other)
    {
        return maybe.Equals(other);
    }

    public static bool operator !=(Maybe<T> maybe, object other)
    {
        return !(maybe == other);
    }

    public static bool operator ==(Maybe<T> first, Maybe<T> second)
    {
        return first.Equals(second);
    }

    public static bool operator !=(Maybe<T> first, Maybe<T> second)
    {
        return !(first == second);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (obj is Maybe<T> other)
            return Equals(other);
        if (obj is T value)
            return Equals(value);
        return false;
    }

    public bool Equals(Maybe<T> other)
    {
        if (HasNoValue && other.HasNoValue)
            return true;

        if (HasNoValue || other.HasNoValue)
            return false;

        return EqualityComparer<T>.Default.Equals(_value, other._value);
    }

    public static Maybe<T> From(T? obj)
    {
        return new Maybe<T>(obj);
    }
    public override int GetHashCode()
    {
        if (HasNoValue)
            return 0;

        return _value!.GetHashCode();
    }
    public T? GetValueOrDefault(T? defaultValue = default)
    {
        if (HasNoValue)
            return defaultValue;

        return _value!;
    }

    public T GetValueOrThrow(string? errorMessage = null)
    {
        if (HasNoValue)
            throw new InvalidOperationException(errorMessage ?? "Maybe has no value.");

        return _value!;
    }

    public T GetValueOrThrow(Exception exception)
    {
        if (HasNoValue)
            throw exception;

        return _value!;
    }
    public override string ToString()
    {
        if (HasNoValue)
            return "No value";

        return _value!.ToString() ?? "No Value";
    }
    public bool TryGetValue([NotNullWhen(true), MaybeNullWhen(false)] out T value)
    {
        value = _value;
        return _isValueSet;
    }
}

public readonly struct Maybe
{
    public static Maybe None => new();

    public static Maybe<T> From<T>(T? value) => Maybe<T>.From(value);
}

public interface IMaybe<out T>
{
    T Value { get; }
    bool HasValue { get; }
    bool HasNoValue { get; }
}
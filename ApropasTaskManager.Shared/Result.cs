using System;
using System.Collections.Generic;
using System.Text;

namespace ApropasTaskManager.Shared
{
    /// <summary>
    /// I hate use try/catch, thats why i create that struct.
    /// If success result return void, use <see cref="System.Reactive.Unit"/> as T.
    /// if you want use <see cref="ApropasTaskManager.Shared.Result{T}.Value"/> as bool, you need explicit use Error and Value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public readonly struct Result<T>
    {
        public static Result<T> CreateError(object error) => new Result<T>(default, error);
        public Result(T value, object error = null)
        {
            Value = value;
            Error = error;
        }

        public T Value { get; }
        public bool IsSuccess => Error == null;
        public object Error { get; }

        public Result<U> ToError<U>(object error = null)
        {
            return Result<U>.CreateError(error ?? Error);
        }

        public Result<U> ResultOrErrorIfNull<U>(Func<T, U> creator, object nullError = null)
        {
            if (!IsSuccess)
            {
                return ToError<U>();
            }
            if (Value == null)
            {
                return ToError<U>(nullError);
            }

            return creator(Value);
        }

        public Result<U> ResultOrError<U>(Func<T, U> creator)
        {
            if (!IsSuccess)
            {
                return ToError<U>();
            }

            return creator(Value);
        }


        public static implicit operator T(Result<T> result)
        {
            return result.Value;
        }

        public static implicit operator bool(Result<T> result)
        {
            return result.IsSuccess;
        }

        public static implicit operator Result<T>(T value)
        {
            return new Result<T>(value);
        }
    }
}

using System;

namespace Cranks.Result
{
    // TODO: Should have the same overloads as ResultBuilder
    public static partial class ResultExtensions
    {
        public static IResult WithErrorIf(this IResult result, bool condition, Error error)
            => condition ? result.WithError(error) : result;

        public static IResult WithErrorIf(this IResult result,
                                          bool condition,
                                          Error error,
                                          Success orSuccess)
            => condition ? result.WithError(error) : result.WithSuccess(orSuccess);

        public static IResult<TValue> WithErrorIf<TValue>(this IResult<TValue> result, bool condition, Error error)
            => condition ? result.WithError(error) : result;

        public static IResult<TValue> WithErrorIf<TValue>(this IResult<TValue> result,
                                                          bool condition,
                                                          Error error,
                                                          Success orSuccess)
            => condition ? result.WithError(error) : result.WithSuccess(orSuccess);

        public static IResult WithErrorIf(this IResult result, bool condition, Func<Error> error)
            => condition ? result.WithError(error()) : result;

        public static IResult WithErrorIf(this IResult result,
                                          bool condition,
                                          Func<Error> error,
                                          Func<Success> orSuccess)
            => condition ? result.WithError(error()) : result.WithSuccess(orSuccess());

        public static IResult<TValue> WithErrorIf<TValue>(this IResult<TValue> result, bool condition, Func<Error> error)
            => condition ? result.WithError(error()) : result;

        public static IResult<TValue> WithErrorIf<TValue>(this IResult<TValue> result,
                                                          bool condition,
                                                          Func<Error> error,
                                                          Func<Success> orSuccess)
            => condition ? result.WithError(error()) : result.WithSuccess(orSuccess());
    }
}

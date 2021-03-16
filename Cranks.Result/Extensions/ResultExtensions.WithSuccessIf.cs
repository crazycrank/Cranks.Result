using System;

namespace Cranks.Result
{
    // TODO: Should have the same overloads as ResultBuilder
    public static partial class ResultExtensions
    {
        public static IResult WithSuccessIf(this IResult result, bool condition, Success success)
            => condition ? result.WithSuccess(success) : result;

        public static IResult WithSuccessIf(this IResult result,
                                            bool condition,
                                            Success success,
                                            Error orError)
            => condition ? result.WithSuccess(success) : result.WithError(orError);

        public static IResult<TValue> WithSuccessIf<TValue>(this IResult<TValue> result, bool condition, Success success)
            => condition ? result.WithSuccess(success) : result;

        public static IResult<TValue> WithSuccessIf<TValue>(this IResult<TValue> result,
                                                            bool condition,
                                                            Success success,
                                                            Error orError)
            => condition ? result.WithSuccess(success) : result.WithError(orError);

        public static IResult WithSuccessIf(this IResult result, bool condition, Func<Success> success)
            => condition ? result.WithSuccess(success()) : result;

        public static IResult WithSuccessIf(this IResult result,
                                            bool condition,
                                            Func<Success> success,
                                            Func<Error> orError)
            => condition ? result.WithSuccess(success()) : result.WithError(orError());

        public static IResult<TValue> WithSuccessIf<TValue>(this IResult<TValue> result, bool condition, Func<Success> success)
            => condition ? result.WithSuccess(success()) : result;

        public static IResult<TValue> WithSuccessIf<TValue>(this IResult<TValue> result,
                                                            bool condition,
                                                            Func<Success> success,
                                                            Func<Error> orError)
            => condition ? result.WithSuccess(success()) : result.WithError(orError());
    }
}

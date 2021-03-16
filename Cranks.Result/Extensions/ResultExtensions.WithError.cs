using System.Collections.Generic;
using System.Linq;

namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        public static IResult WithError(this IResult result, params Error[] errors) => result.WithError(errors.AsEnumerable());
        public static IResult WithError(this IResult result, IEnumerable<Error> errors) => result.WithReason(errors);
        public static IResult WithError<TError>(this IResult result)
            where TError : Error, new()
            => result.WithError(new TError());

        public static IResult<TValue> WithError<TValue>(this IResult<TValue> result, params Error[] errors) => result.WithError(errors.AsEnumerable());
        public static IResult<TValue> WithError<TValue>(this IResult<TValue> result, IEnumerable<Error> errors) => result.WithReason(errors);
        public static IResult<TValue> WithError<TError, TValue>(this IResult<TValue> result)
            where TError : Error, new()
            => result.WithError(new TError());
    }
}

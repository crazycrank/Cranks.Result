using System.Collections.Generic;
using System.Linq;

namespace ResultZ
{
    public static partial class ResultExtensions
    {
        public static IResult<TValue> WithError<TValue>(this IResult<TValue> result, params Error[] successes) => result.WithError(successes.AsEnumerable());
        public static IResult<TValue> WithError<TValue>(this IResult<TValue> result, IEnumerable<Error> successes) => result.WithReason(successes);
        public static IResult<TValue> WithError<TError, TValue>(this IResult<TValue> result)
            where TError : Error, new()
            => result.WithError(new TError());
    }
}

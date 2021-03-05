using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using ResultZ.Reasons;

namespace ResultZ.Results
{
    public static partial class ResultExtensions
    {
        public static IResult WithError(this IResult result, params string[] messages) => result.WithError(messages.AsEnumerable());
        public static IResult WithError(this IResult result, IEnumerable<string> messages) => result.WithError(messages.Select(m => new Error(m)));
        public static IResult WithError(this IResult result, params Error[] successes) => result.WithError(successes.AsEnumerable());
        public static IResult WithError(this IResult result, IEnumerable<Error> successes) => result.WithReason(successes);
        public static IResult WithError<TError>(this IResult result)
            where TError : Error, new()
            => result.WithError(new TError());
    }
}

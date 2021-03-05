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
        public static IResult<TValue> WithError<TValue>(this IResult<TValue> result, params string[] messages) => result.WithError(messages.AsEnumerable());
        public static IResult<TValue> WithError<TValue>(this IResult<TValue> result, IEnumerable<string> messages) => result.WithError(messages.Select(m => new Error(m)));
        public static IResult<TValue> WithError<TValue>(this IResult<TValue> result, params Error[] successes) => result.WithError(successes.AsEnumerable());
        public static IResult<TValue> WithError<TValue>(this IResult<TValue> result, IEnumerable<Error> successes) => result.WithReason(successes);
        public static IResult<TValue> WithError<TError, TValue>(this IResult<TValue> result)
            where TError : Error, new()
            => result.WithError(new TError());
    }
}

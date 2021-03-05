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
        public static IResult<TValue> WithSuccess<TValue>(this IResult<TValue> result, params string[] messages) => result.WithSuccess(messages.AsEnumerable());
        public static IResult<TValue> WithSuccess<TValue>(this IResult<TValue> result, IEnumerable<string> messages) => result.WithSuccess(messages.Select(m => new Success(m)));
        public static IResult<TValue> WithSuccess<TValue>(this IResult<TValue> result, params Success[] successes) => result.WithSuccess(successes.AsEnumerable());
        public static IResult<TValue> WithSuccess<TValue>(this IResult<TValue> result, IEnumerable<Success> successes) => result.WithReason(successes);
        public static IResult<TValue> WithSuccess<TSuccess, TValue>(this IResult<TValue> result)
            where TSuccess : Success, new()
            => result.WithSuccess(new TSuccess());
    }
}

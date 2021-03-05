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
        //public static IResult WithCause(this IResult result, IResult cause) => result.WithReason(messages.AsEnumerable());
        //public static IResult WithSuccess(this IResult result, IEnumerable<string> messages) => result.WithSuccess(messages.Select(m => new Success(m)));
        //public static IResult WithSuccess(this IResult result, params Success[] successes) => result.WithSuccess(successes.AsEnumerable());
        //public static IResult WithSuccess(this IResult result, IEnumerable<Success> successes) => result.WithReason(successes);
        //public static IResult WithSuccess<TSuccess>(this IResult result)
        //    where TSuccess : Success, new()
        //    => result.WithSuccess(new TSuccess());
    }
}

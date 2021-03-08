using System;
using System.Collections.Generic;
using System.Linq;

namespace ResultZ
{
    public static partial class ResultExtensions
    {
        public static IResult WithReason(this IResult result, params IReason[] reasons) => result.WithReason(reasons.AsEnumerable());
        public static IResult WithReason(this IResult result, IEnumerable<IReason> reasons) => reasons.Aggregate(result, (r, reason) => r.WithSingleReason(reason));
        public static IResult<TValue> WithReason<TValue>(this IResult<TValue> result, params IReason[] reasons) => result.WithReason(reasons.AsEnumerable());
        public static IResult<TValue> WithReason<TValue>(this IResult<TValue> result, IEnumerable<IReason> reasons)
            => reasons.Aggregate(result, (r, reason) => r.WithSingleReason(reason));

        private static IResult<TValue> WithSingleReason<TValue>(this IResult<TValue> result, IReason reason)
        {
            return (result, reason) switch
                   {
                       { result: Failed<TValue> } or { reason: Error } => new Failed<TValue>(result.Message, result.Reasons.Append(reason)),
                       { result: Passed<TValue> sucessful } => new Passed<TValue>(sucessful.Value, result.Message, sucessful.Reasons.Append(reason)),
                   };
        }

        private static IResult WithSingleReason(this IResult result, IReason reason)
        {
            // dogfooding
            // TODO: is it possible that the compiler realizes this is exhaustive?
            return HandleGenericVariant(result, nameof(WithSingleReason), reason) switch
                   {
                       Passed<IResult> { Value: not null and var genericResult } => genericResult,
                       Failed => (result, reason) switch
                                 {
                                     { result: Failed } or { reason: Error } => new Failed(result.Message, result.Reasons.Append(reason)),
                                     { result: Passed sucessful } => new Passed(result.Message, sucessful.Reasons.Append(reason)),
                                 },
                   };
        }
    }
}

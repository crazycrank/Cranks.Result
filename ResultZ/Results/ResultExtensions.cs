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
        internal static IResult WithReason(this IResult result, params Reason[] reasons) => result.WithReason(reasons.AsEnumerable());
        internal static IResult WithReason(this IResult result, IEnumerable<Reason> reasons) => reasons.Aggregate(result, (r, reason) => r.WithSingleReason(reason));
        internal static IResult<TValue> WithReason<TValue>(this IResult<TValue> result, params Reason[] reasons) => result.WithReason(reasons.AsEnumerable());
        internal static IResult<TValue> WithReason<TValue>(this IResult<TValue> result, IEnumerable<Reason> reasons) => reasons.Aggregate(result, (r, reason) => r.WithSingleReason(reason));

        private static IResult<TValue> WithSingleReason<TValue>(this IResult<TValue> result, Reason reason)
        {
            return (result, reason) switch
                   {
                       { result: Failure<TValue> } or { reason: Error } => new Failure<TValue>(result, reason),
                       { result: Successful<TValue> sucessful } => new Successful<TValue>(sucessful, reason),
                       _ => throw new InvalidOperationException("Unexpected combination"),
                   };
        }

        private static IResult WithSingleReason(this IResult result, Reason reason)
        {
            // dogfooding
            // TODO: is it possible that the compiler realizes this is exhaustive?
            return HandleGenericVariant(result, reason) switch
                   {
                       Successful<IResult> { Value: var genericResult } => genericResult,
                       Failure => (result, reason) switch
                                  {
                                      { result: Failure } or { reason: Error } => new Failure(result, reason),
                                      { result: Successful sucessful } => new Successful(sucessful, reason),
                                  },
                   };
        }

        private static IResult<IResult> HandleGenericVariant(this IResult result, Reason reason)
        {
            // dogfooding
            return GetGenericSingleReasonMethod(result.GetType()) switch
                   {
                       Failure<MethodInfo> => Result.Failure<IResult>(),
                       Successful<MethodInfo> { Value: var singleReasonMethod }
                           => Result.Successful((IResult)singleReasonMethod.Invoke(null, new object[] { result, reason })!),
                   };
        }

#pragma warning disable SA1201 // Elements should appear in the correct order
        private static readonly ConcurrentDictionary<Type, MethodInfo> WithSingleReasonCache = new();
#pragma warning restore SA1201 // Elements should appear in the correct order

        private static IResult<MethodInfo> GetGenericSingleReasonMethod(Type resultType)
        {
            if (!resultType.IsGenericType)
            {
                return Result.Failure<MethodInfo>();
            }

            var resultValueType = resultType.GetGenericArguments().Single();
            var genericResultType = typeof(IResult<>).MakeGenericType(resultValueType);
            if (!resultType.IsAssignableTo(genericResultType))
            {
                return Result.Failure<MethodInfo>();
            }

            var singleReasonMethod = WithSingleReasonCache.GetOrAdd(resultType, CreateGenericSingleReasonsMethod());

            return Result.Successful(singleReasonMethod);

            MethodInfo CreateGenericSingleReasonsMethod()
            {
                return typeof(ResultExtensions)
                       .GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                       .Single(m => m.Name == nameof(WithSingleReason) && m.IsGenericMethod)
                       .MakeGenericMethod(resultValueType);
            }
        }
    }
}

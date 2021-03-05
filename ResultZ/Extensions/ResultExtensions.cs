using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ResultZ
{
    public static partial class ResultExtensions
    {
        internal static IResult WithReason(this IResult result, params IReason[] reasons) => result.WithReason(reasons.AsEnumerable());
        internal static IResult WithReason(this IResult result, IEnumerable<IReason> reasons) => reasons.Aggregate(result, (r, reason) => r.WithSingleReason(reason));
        internal static IResult<TValue> WithReason<TValue>(this IResult<TValue> result, params IReason[] reasons) => result.WithReason(reasons.AsEnumerable());
        internal static IResult<TValue> WithReason<TValue>(this IResult<TValue> result, IEnumerable<IReason> reasons) => reasons.Aggregate(result, (r, reason) => r.WithSingleReason(reason));

        private static IResult<TValue> WithSingleReason<TValue>(this IResult<TValue> result, IReason reason)
        {
            return (result, reason) switch
                   {
                       { result: Failed<TValue> } or { reason: Error } => new Failed<TValue>(result.Message, result.Reasons.Append(reason)),
                       { result: Passed<TValue> sucessful } => new Passed<TValue>(sucessful.Value, result.Message, sucessful.Reasons.Append(reason)),
                       _ => throw new InvalidOperationException("Deriving from IResult is not supported"),
                   };
        }

        private static IResult WithSingleReason(this IResult result, IReason reason)
        {
            // dogfooding
            // TODO: is it possible that the compiler realizes this is exhaustive?
            return HandleGenericVariant(result, reason) switch
                   {
                       Passed<IResult> { Value: var genericResult } => genericResult,
                       Failed => (result, reason) switch
                                  {
                                      { result: Failed } or { reason: Error } => new Failed(result.Message, result.Reasons.Append(reason)),
                                      { result: Passed sucessful } => new Passed(result.Message, sucessful.Reasons.Append(reason)),
                                      _ => throw new InvalidOperationException("Deriving from IResult is not supported"),
                                  },
                       _ => throw new InvalidOperationException("Deriving from IResult is not supported"),
                   };
        }

        private static IResult<IResult> HandleGenericVariant(this IResult result, IReason reason)
        {
            // dogfooding
            return GetGenericSingleReasonMethod(result.GetType()) switch
                   {
                       Failed => Result.Fail<IResult>(),
                       Passed<MethodInfo> { Value: var singleReasonMethod }
                           => Result.Pass((IResult)singleReasonMethod.Invoke(null, new object[] { result, reason })!),
                       _ => throw new InvalidOperationException("Deriving from IResult is not supported"),
                   };
        }

#pragma warning disable SA1201 // Elements should appear in the correct order
        private static readonly ConcurrentDictionary<Type, MethodInfo> WithSingleReasonCache = new();
#pragma warning restore SA1201 // Elements should appear in the correct order

        private static IResult<MethodInfo> GetGenericSingleReasonMethod(Type resultType)
        {
            if (!resultType.IsGenericType)
            {
                return Result.Fail<MethodInfo>();
            }

            var resultValueType = resultType.GetGenericArguments().Single();
            var genericResultType = typeof(IResult<>).MakeGenericType(resultValueType);
            if (!resultType.IsAssignableTo(genericResultType))
            {
                return Result.Fail<MethodInfo>();
            }

            var singleReasonMethod = WithSingleReasonCache.GetOrAdd(resultType, CreateGenericSingleReasonsMethod());

            return Result.Pass(singleReasonMethod);

            MethodInfo CreateGenericSingleReasonsMethod()
            {
                // TODO maybe create a func from the method info
                ////Expression.Lambda<Func<IResult<TValue>, Reason, IResult>()
                var genericSingleReasonsMethod = typeof(ResultExtensions)
                                                 .GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                                                 .Single(m => m.Name == nameof(WithSingleReason) && m.IsGenericMethod)
                                                 .MakeGenericMethod(resultValueType);
                return genericSingleReasonsMethod;
            }
        }
    }
}

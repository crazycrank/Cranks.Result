using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ResultZ.Reasons;

namespace ResultZ.Results
{
    public static class ResultExtensions
    {

        internal static IResult WithReason(this IResult result, params Reason[] reasons)
            => result.WithReason(reasons.AsEnumerable());
        internal static IResult WithReason(this IResult result, IEnumerable<Reason> reasons)
            => reasons.Aggregate(result, (r, reason) => r.WithSingleReason(reason));
        internal static IResult<TValue> WithReason<TValue>(this IResult<TValue> result, params Reason[] reasons)
            => result.WithReason(reasons.AsEnumerable());
        internal static IResult<TValue> WithReason<TValue>(this IResult<TValue> result, IEnumerable<Reason> reasons)
            => reasons.Aggregate(result, (r, reason) => r.WithSingleReason(reason));

        public static IResult WithSuccess(this IResult result, params string[] messages)
            => result.WithSuccess(messages.AsEnumerable());
        public static IResult WithSuccess(this IResult result, IEnumerable<string> messages)
            => result.WithSuccess(messages.Select(m => new Success(m)));
        public static IResult WithSuccess<TSuccess>(this IResult result)
            where TSuccess : Success, new()
            => result.WithSuccess(new TSuccess());
        public static IResult WithSuccess(this IResult result, params Success[] successes)
            => result.WithSuccess(successes.AsEnumerable());
        public static IResult WithSuccess(this IResult result, IEnumerable<Success> successes)
            => result.WithReason(successes);

        public static IResult<TValue> WithSuccess<TValue>(this IResult<TValue> result, params string[] messages)
            => result.WithSuccess(messages.AsEnumerable());
        public static IResult<TValue> WithSuccess<TValue>(this IResult<TValue> result, IEnumerable<string> messages)
            => result.WithSuccess(messages.Select(m => new Success(m)));
        public static IResult<TValue> WithSuccess<TSuccess, TValue>(this IResult<TValue> result)
            where TSuccess : Success, new()
            => result.WithSuccess(new TSuccess());
        public static IResult<TValue> WithSuccess<TValue>(this IResult<TValue> result, params Success[] successes)
            => result.WithSuccess(successes.AsEnumerable());
        public static IResult<TValue> WithSuccess<TValue>(this IResult<TValue> result, IEnumerable<Success> successes)
            => result.WithReason(successes);

        public static IResult WithError(this IResult result, params string[] messages)
            => result.WithError(messages.AsEnumerable());
        public static IResult WithError(this IResult result, IEnumerable<string> messages)
            => result.WithError(messages.Select(m => new Error(m)));
        public static IResult WithError<TError>(this IResult result)
            where TError : Error, new()
            => result.WithError(new TError());
        public static IResult WithError(this IResult result, params Error[] successes)
            => result.WithError(successes.AsEnumerable());
        public static IResult WithError(this IResult result, IEnumerable<Error> successes)
            => result.WithReason(successes);

        public static IResult<TValue> WithError<TValue>(this IResult<TValue> result, params string[] messages)
            => result.WithError(messages.AsEnumerable());
        public static IResult<TValue> WithError<TValue>(this IResult<TValue> result, IEnumerable<string> messages)
            => result.WithError(messages.Select(m => new Error(m)));
        public static IResult<TValue> WithError<TError, TValue>(this IResult<TValue> result)
            where TError : Error, new()
            => result.WithError(new TError());
        public static IResult<TValue> WithError<TValue>(this IResult<TValue> result, params Error[] successes)
            => result.WithError(successes.AsEnumerable());
        public static IResult<TValue> WithError<TValue>(this IResult<TValue> result, IEnumerable<Error> successes)
            => result.WithReason(successes);

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
                    {result: Failure} or {reason: Error} => new Failure(result, reason),
                    {result: Successful sucessful} => new Successful(sucessful, reason),
                },
            };
        }

        private static IResult<IResult> HandleGenericVariant(this IResult result, Reason reason)
        {
            return GetGenericSingleReasonMethod(result.GetType()) switch
            {
                Failure<MethodInfo> failure => Result.From<IResult, MethodInfo>(failure),
                Successful<MethodInfo> {Value: var singleReasonMethod} 
                    => Result.Successful((IResult) singleReasonMethod.Invoke(null, new object[] {result, reason})!),
            };
        }

        private static readonly Dictionary<Type, MethodInfo> WithSingleReasonCache = new();

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

            // TODO Thread safety?
            if (!WithSingleReasonCache.ContainsKey(resultType))
            {
                WithSingleReasonCache.Add(resultType, CreateGenericSingleReasonsMethod());
            }

            return Result.Successful(WithSingleReasonCache[resultType]);

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

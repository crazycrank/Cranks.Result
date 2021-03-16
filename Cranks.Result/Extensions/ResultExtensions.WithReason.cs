using System.Collections.Generic;
using System.Linq;

namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new instance of <see cref="IResult"/> with <paramref name="reasons"/> appended to <see cref="IReason.Reasons"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="reasons">The <see cref="IReason"/>s to append to the result.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithReason(this IResult result, params IReason[] reasons) => result.WithReason(reasons.AsEnumerable());

        /// <summary>
        /// Creates a new instance of <see cref="IResult"/> with <paramref name="reasons"/> appended to <see cref="IReason.Reasons"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="reasons">The <see cref="IReason"/>s to append to the result.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithReason(this IResult result, IEnumerable<IReason> reasons) => reasons.Aggregate(result, (r, reason) => r.WithSingleReason(reason));

        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with <paramref name="reasons"/> appended to <see cref="IReason.Reasons"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="reasons">The <see cref="IReason"/>s to append to the result.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithReason<TValue>(this IResult<TValue> result, params IReason[] reasons) => result.WithReason(reasons.AsEnumerable());

        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with <paramref name="reasons"/> appended to <see cref="IReason.Reasons"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="reasons">The <see cref="IReason"/>s to append to the result.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithReason<TValue>(this IResult<TValue> result, IEnumerable<IReason> reasons)
            => reasons.Aggregate(result, (r, reason) => r.WithSingleReason(reason));

        private static IResult<TValue> WithSingleReason<TValue>(this IResult<TValue> result, IReason reason)
        {
            return (result, reason) switch
                   {
                       { result: Failed<TValue> } or { reason: Error } => new Failed<TValue>(result.Message, result.Reasons.Append(reason)),
                       { result: Passed<TValue> passed } => new Passed<TValue>(passed.Value, result.Message, passed.Reasons.Append(reason)),
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
                                     { result: Passed passed } => new Passed(result.Message, passed.Reasons.Append(reason)),
                                 },
                   };
        }
    }
}

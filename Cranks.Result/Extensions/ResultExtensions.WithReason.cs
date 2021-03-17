using System.Collections.Generic;
using System.Linq;

namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new instance of <see cref="IResult"/> with <paramref name="causes"/> appended to <see cref="IReason.Causes"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="causes">The <see cref="IReason"/>s to append to the result.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithCause(this IResult result, params IReason[] causes) => result.WithCause(causes.AsEnumerable());

        /// <summary>
        /// Creates a new instance of <see cref="IResult"/> with <paramref name="causes"/> appended to <see cref="IReason.Causes"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="causes">The <see cref="IReason"/>s to append to the result.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithCause(this IResult result, IEnumerable<IReason> causes) => causes.Aggregate(result, (r, cause) => r.WithSingleCause(cause));

        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with <paramref name="causes"/> appended to <see cref="IReason.Causes"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="causes">The <see cref="IReason"/>s to append to the result.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithCause<TValue>(this IResult<TValue> result, params IReason[] causes) => result.WithCause(causes.AsEnumerable());

        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with <paramref name="causes"/> appended to <see cref="IReason.Causes"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="causes">The <see cref="IReason"/>s to append to the result.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithCause<TValue>(this IResult<TValue> result, IEnumerable<IReason> causes)
            => causes.Aggregate(result, (r, cause) => r.WithSingleCause(cause));

        private static IResult<TValue> WithSingleCause<TValue>(this IResult<TValue> result, IReason cause)
            => (result, cause) switch
               {
                   { result: Failed<TValue> } or { cause: Error } => new Failed<TValue>(result.Message, result.Causes.Append(cause)),
                   { result: Passed<TValue> passed } => new Passed<TValue>(passed.Value, result.Message, passed.Causes.Append(cause)),
               };

        private static IResult WithSingleCause(this IResult result, IReason cause)
            => HandleGenericVariant(result, nameof(WithSingleCause), cause) switch
               {
                   Passed<IResult> { Value: var genericResult } => genericResult,
                   Failed => (result, cause) switch
                             {
                                 { result: Failed } or { cause: Error } => new Failed(result.Message, result.Causes.Append(cause)),
                                 { result: Passed passed } => new Passed(result.Message, passed.Causes.Append(cause)),
                             },
               };
    }
}

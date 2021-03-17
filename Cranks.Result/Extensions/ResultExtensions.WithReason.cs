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

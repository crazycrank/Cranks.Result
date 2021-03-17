using System.Collections.Generic;
using System.Linq;

namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new instance of <see cref="IResult"/> with <paramref name="successes"/> appended to <see cref="IReason.Causes"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="successes">The <see cref="Success"/>es to append to the result.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithSuccess(this IResult result, params Success[] successes) => result.WithSuccess(successes.AsEnumerable());

        /// <summary>
        /// Creates a new instance of <see cref="IResult"/> with <paramref name="successes"/> appended to <see cref="IReason.Causes"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="successes">The <see cref="Success"/>es to append to the result.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithSuccess(this IResult result, IEnumerable<Success> successes) => result.WithCause(successes);

        /// <summary>
        /// Creates a new instance of <see cref="IResult"/> with an instance of <typeparamref name="TSuccess"/> appended to <see cref="IReason.Causes"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <typeparam name="TSuccess">Type of the <see cref="Success"/> to create.</typeparam>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithSuccess<TSuccess>(this IResult result)
            where TSuccess : Success, new()
            => result.WithSuccess(new TSuccess());
    }
}

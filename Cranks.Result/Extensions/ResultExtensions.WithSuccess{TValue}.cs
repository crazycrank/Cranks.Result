using System.Collections.Generic;
using System.Linq;

namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with <paramref name="successes"/> appended to <see cref="IReason.Causes"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="successes">The <see cref="Success"/>es to append to the result.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithSuccess<TValue>(this IResult<TValue> result, params Success[] successes) => result.WithSuccess(successes.AsEnumerable());

        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with <paramref name="successes"/> appended to <see cref="IReason.Causes"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="successes">The <see cref="Success"/>es to append to the result.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithSuccess<TValue>(this IResult<TValue> result, IEnumerable<Success> successes) => result.WithCause(successes);

        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with an instance of <typeparamref name="TSuccess"/> appended to <see cref="IReason.Causes"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <typeparam name="TSuccess">Type of the <see cref="Success"/> to create.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithSuccess<TValue, TSuccess>(this IResult<TValue> result)
            where TSuccess : Success, new()
            => result.WithSuccess(new TSuccess());
    }
}

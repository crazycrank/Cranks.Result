using System.Collections.Generic;
using System.Linq;

namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with <paramref name="errors"/> appended to <see cref="IReason.Causes"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="errors">The <see cref="Error"/>s to append to the result.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithError<TValue>(this IResult<TValue> result, params Error[] errors) => result.WithError(errors.AsEnumerable());

        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with <paramref name="errors"/> appended to <see cref="IReason.Causes"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="errors">The <see cref="Error"/>s to append to the result.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithError<TValue>(this IResult<TValue> result, IEnumerable<Error> errors) => result.WithCause(errors);

        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with an instance of <typeparamref name="TError"/> appended to <see cref="IReason.Causes"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <typeparam name="TError">Type of the <see cref="Error"/> to create.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithError<TValue, TError>(this IResult<TValue> result)
            where TError : Error, new()
            => result.WithError(new TError());
    }
}

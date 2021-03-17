using System.Collections.Generic;
using System.Linq;

namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new instance of <see cref="IResult"/> with <paramref name="errors"/> appended to <see cref="IReason.Causes"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="errors">The <see cref="Error"/>s to append to the result.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithError(this IResult result, params Error[] errors) => result.WithError(errors.AsEnumerable());

        /// <summary>
        /// Creates a new instance of <see cref="IResult"/> with <paramref name="errors"/> appended to <see cref="IReason.Causes"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="errors">The <see cref="Error"/>s to append to the result.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithError(this IResult result, IEnumerable<Error> errors) => result.WithCause(errors);

        /// <summary>
        /// Creates a new instance of <see cref="IResult"/> with an instance of <typeparamref name="TError"/> appended to <see cref="IReason.Causes"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <typeparam name="TError">Type of the <see cref="Error"/> to create.</typeparam>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithError<TError>(this IResult result)
            where TError : Error, new()
            => result.WithError(new TError());
    }
}

using System;

namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new <see cref="IResult"/> with <paramref name="success"/> added to <see cref="IReason.Causes"/> if <paramref name="condition"/> is true.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="condition"><paramref name="error"/> is only added to the returned object if this is true.</param>
        /// <param name="success">The <see cref="Success"/> that is added when <paramref name="condition"/> is true.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithSuccessIf(this IResult result, bool condition, Success success)
            => condition ? result.WithSuccess(success) : result.Pass();

        /// <summary>
        /// Creates a new instance of <see cref="IResult"/> with <paramref name="success"/> added to <see cref="IReason.Causes"/> if <paramref name="condition"/> is true.
        /// If <paramref name="condition"/> is false, <paramref name="orError"/> is instead added to the <see cref="IReason.Causes"/> collection.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="condition">Decides if <paramref name="error"/> or <paramref name="orSuccess"/> should be added to the returned object.</param>
        /// <param name="success">The <see cref="Success"/> that is added when <paramref name="condition"/> is true.</param>
        /// <param name="orError">The <see cref="Error"/> that is added when <paramref name="condition"/> is false.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithSuccessIf(this IResult result,
                                            bool condition,
                                            Success success,
                                            Error orError)
            => condition ? result.WithSuccess(success) : result.WithError(orError);

        /// <summary>
        /// Creates a new <see cref="IResult"/> with <paramref name="successFactory"/> added to <see cref="IReason.Causes"/> if <paramref name="condition"/> is true.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="condition"><paramref name="error"/> is only added to the returned object if this is true.</param>
        /// <param name="successFactory">The <see cref="Success"/> that is added when <paramref name="condition"/> is true. </param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithSuccessIf(this IResult result, bool condition, Func<Success> successFactory)
            => condition ? result.WithSuccess(successFactory()) : result.Pass();

        /// <summary>
        /// Creates a new instance of <see cref="IResult"/> with <paramref name="successFactory"/> added to <see cref="IReason.Causes"/> if <paramref name="condition"/> is true.
        /// If <paramref name="condition"/> is false, <paramref name="orErrorFactory"/> is instead added to the <see cref="IReason.Causes"/> collection.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="condition">Decides if <paramref name="error"/> or <paramref name="orSuccess"/> should be added to the returned object.</param>
        /// <param name="successFactory">The <see cref="Success"/> that is added when <paramref name="condition"/> is true.</param>
        /// <param name="orErrorFactory">The <see cref="Error"/> that is added when <paramref name="condition"/> is false.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithSuccessIf(this IResult result,
                                            bool condition,
                                            Func<Success> successFactory,
                                            Func<Error> orErrorFactory)
            => condition ? result.WithSuccess(successFactory()) : result.WithError(orErrorFactory());
    }
}

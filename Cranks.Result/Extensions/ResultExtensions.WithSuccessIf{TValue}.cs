using System;

namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with <paramref name="success"/> added to <see cref="IReason.Causes"/> if <paramref name="condition"/> is true.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="condition"><paramref name="success"/> is only added to the returned object if this is true.</param>
        /// <param name="success">The <see cref="Success"/> that is added when <paramref name="condition"/> is true.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithSuccessIf<TValue>(this IResult<TValue> result, bool condition, Success success)
            => condition ? result.WithSuccess(success) : result.Pass();

        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with <paramref name="success"/> added to <see cref="IReason.Causes"/> if <paramref name="condition"/> is true.
        /// If <paramref name="condition"/> is false, <paramref name="orSuccess"/> is instead added to the <see cref="IReason.Causes"/> collection.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="condition">Decides if <paramref name="success"/> or <paramref name="orError"/> should be added to the returned object.</param>
        /// <param name="success">The <see cref="Success"/> that is added when <paramref name="condition"/> is true.</param>
        /// <param name="orError">The <see cref="Error"/> that is added when <paramref name="condition"/> is false.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithSuccessIf<TValue>(this IResult<TValue> result,
                                                            bool condition,
                                                            Success success,
                                                            Error orError)
            => condition ? result.WithSuccess(success) : result.WithError(orError);

        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with <paramref name="successFactory"/> added to <see cref="IReason.Causes"/> if <paramref name="condition"/> is true.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="condition"><paramref name="successFactory"/> is only added to the returned object if this is true.</param>
        /// <param name="successFactory">The <see cref="Success"/> that is added when <paramref name="condition"/> is true.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithSuccessIf<TValue>(this IResult<TValue> result, bool condition, Func<Success> successFactory)
            => condition ? result.WithSuccess(successFactory()) : result.Pass();

        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with <paramref name="successFactory"/> added to <see cref="IReason.Causes"/> if <paramref name="condition"/> is true.
        /// If <paramref name="condition"/> is false, <paramref name="orErrorFactory"/> is instead added to the <see cref="IReason.Causes"/> collection.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="condition">Decides if <paramref name="successFactory"/> or <paramref name="orErrorFactory"/> should be added to the returned object.</param>
        /// <param name="successFactory">The <see cref="Success"/> that is added when <paramref name="condition"/> is true.</param>
        /// <param name="orErrorFactory">The <see cref="Error"/> that is added when <paramref name="condition"/> is false.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithSuccessIf<TValue>(this IResult<TValue> result,
                                                            bool condition,
                                                            Func<Success> successFactory,
                                                            Func<Error> orErrorFactory)
            => condition ? result.WithSuccess(successFactory()) : result.WithError(orErrorFactory());
    }
}

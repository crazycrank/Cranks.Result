using System;

namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with <paramref name="error"/> added to <see cref="IReason.Causes"/> if <paramref name="condition"/> is true.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="condition"><paramref name="error"/> is only added to the returned object if this is true.</param>
        /// <param name="error">The <see cref="Error"/> that is added when <paramref name="condition"/> is true.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithErrorIf<TValue>(this IResult<TValue> result, bool condition, Error error)
            => condition ? result.WithError(error) : result.Pass();

        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with <paramref name="error"/> added to <see cref="IReason.Causes"/> if <paramref name="condition"/> is true.
        /// If <paramref name="condition"/> is false, <paramref name="orSuccess"/> is instead added to the <see cref="IReason.Causes"/> collection.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="condition">Decides if <paramref name="error"/> or <paramref name="orSuccess"/> should be added to the returned object.</param>
        /// <param name="error">The <see cref="Error"/> that is added when <paramref name="condition"/> is true.</param>
        /// <param name="orSuccess">The <see cref="Success"/> that is added when <paramref name="condition"/> is false.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithErrorIf<TValue>(this IResult<TValue> result,
                                                          bool condition,
                                                          Error error,
                                                          Success orSuccess)
            => condition ? result.WithError(error) : result.WithSuccess(orSuccess);

        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with <paramref name="errorFactory"/> added to <see cref="IReason.Causes"/> if <paramref name="condition"/> is true.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="condition"><paramref name="errorFactory"/> is only added to the returned object if this is true.</param>
        /// <param name="errorFactory">The <see cref="Error"/> that is added when <paramref name="condition"/> is true.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithErrorIf<TValue>(this IResult<TValue> result, bool condition, Func<Error> errorFactory)
            => condition ? result.WithError(errorFactory()) : result.Pass();

        /// <summary>
        /// Creates a new instance of <see cref="IResult{TValue}"/> with <paramref name="errorFactory"/> added to <see cref="IReason.Causes"/> if <paramref name="condition"/> is true.
        /// If <paramref name="condition"/> is false, <paramref name="orSuccessFactory"/> is instead added to the <see cref="IReason.Causes"/> collection.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="condition">Decides if <paramref name="errorFactory"/> or <paramref name="orSuccessFactory"/> should be added to the returned object.</param>
        /// <param name="errorFactory">The <see cref="Error"/> that is added when <paramref name="condition"/> is true.</param>
        /// <param name="orSuccessFactory">The <see cref="Success"/> that is added when <paramref name="condition"/> is false.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithErrorIf<TValue>(this IResult<TValue> result,
                                                          bool condition,
                                                          Func<Error> errorFactory,
                                                          Func<Success> orSuccessFactory)
            => condition ? result.WithError(errorFactory()) : result.WithSuccess(orSuccessFactory());
    }
}

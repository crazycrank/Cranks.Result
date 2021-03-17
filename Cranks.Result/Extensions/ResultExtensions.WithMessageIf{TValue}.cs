namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new <see cref="IResult{TValue}"/> with <see cref="IResult.Message"/> set to <paramref name="message"/> if <paramref name="condition"/> is true.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="condition"><paramref name="message"/> is only added to the returned object if this is true.</param>
        /// <param name="message">The new message of <see cref="IResult"/> if <paramref name="condition"/> is true.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithMessageIf<TValue>(this IResult<TValue> result, bool condition, string message) => condition ? result.WithMessage(message) : result.Pass();

        /// <summary>
        /// Creates a new <see cref="IResult{TValue}"/> with <see cref="IResult.Message"/> set to <paramref name="message"/> if <paramref name="condition"/> is true.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="condition"><paramref name="message"/> is only added to the returned object if this is true.</param>
        /// <param name="message">The new message of <see cref="IResult{TValue}"/> if <paramref name="condition"/> is true.</param>
        /// <param name="orMessage">The new message of <see cref="IResult{TValue}"/> if <paramref name="condition"/> is false.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithMessageIf<TValue>(this IResult<TValue> result,
                                                            bool condition,
                                                            string message,
                                                            string orMessage)
            => condition ? result.WithMessage(message) : result.WithMessage(orMessage);
    }
}

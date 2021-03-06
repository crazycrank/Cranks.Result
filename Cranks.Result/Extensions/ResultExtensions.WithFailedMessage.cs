namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new <see cref="IResult"/> with <see cref="IResult.Message"/> set to <paramref name="message"/> if it is of type <see cref="Passed"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="message">The new message of <see cref="IResult"/> if <paramref name="condition"/> is true.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithFailedMessage(this IResult result, string message) => result.WithMessageIf(result is Failed, message);

        /// <summary>
        /// Creates a new <see cref="IResult"/> with <see cref="IResult.Message"/> set to <paramref name="message"/> if it is of type <see cref="Failed"/>.
        /// Otherwise it will be set to <paramref name="orMessage"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="message">The new message of <see cref="IResult"/> if <paramref name="result"/> is of type <see cref="Failed"/>.</param>
        /// <param name="orMessage">The new message of <see cref="IResult"/> if <paramref name="result"/> is of type <see cref="Passed"/>.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithFailedMessage(this IResult result, string message, string orMessage) => result.WithMessageIf(result is Failed, message, orMessage);
    }
}

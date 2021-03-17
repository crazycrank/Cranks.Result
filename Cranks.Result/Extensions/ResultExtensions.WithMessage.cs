namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new <see cref="IResult"/> with <see cref="IResult.Message"/> set to <paramref name="message"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="message">The new message of <see cref="IResult"/>.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult WithMessage(this IResult result, string message)
        => HandleGenericVariant(result, nameof(WithMessage), message) switch
        {
            Passed<IResult> { Value: var genericResult } => genericResult,
            Failed => result switch
            {
                Failed => new Failed(message, result.Causes),
                Passed => new Passed(message, result.Causes),
            },
        };
    }
}

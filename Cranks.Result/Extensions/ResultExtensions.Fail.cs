namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new instance of <see cref="Failed"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult Fail(this IResult result)
            => HandleGenericVariant(result, nameof(Pass)) switch
            {
                Passed<IResult> { Value: var genericResult } => genericResult,
                Failed => new Failed(result.Message, result.Causes),
            };
    }
}

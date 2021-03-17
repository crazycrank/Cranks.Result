namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new instance of <see cref="Passed"/>. If <paramref name="result"/> is already <see cref="Failed"/>,
        /// a new instance of <see cref="Failed"/> is created.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult Pass(this IResult result)
            => HandleGenericVariant(result, nameof(Pass)) switch
            {
                Passed<IResult> { Value: var genericResult } => genericResult,
                Failed => result switch
                          {
                              Passed => new Passed(result.Message, result.Causes),
                              Failed => new Failed(result.Message, result.Causes),
                          },
            };
    }
}

namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new instance of <see cref="Passed"/>, if the <paramref name="condition"/> is true and <paramref name="result"/> is not already <see cref="Failed"/>.
        /// Otherwise a new instance of <see cref="Failed"/> is created.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="condition">If true creates <see cref="Passed"/>, otherwise <see cref="Failed"/>.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult PassIf(this IResult result, bool condition) => condition ? result.Pass() : result.Fail();
    }
}

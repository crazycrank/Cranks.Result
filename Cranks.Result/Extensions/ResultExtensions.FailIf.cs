namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new instance of <see cref="Failed"/> if the <paramref name="condition"/> is true.
        /// Otherwise a new instance of <see cref="Passed"/> is returned.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="condition">If true creates <see cref="Failed"/>, otherwise <see cref="Passed"/>.</param>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult FailIf(this IResult result, bool condition) => condition ? result.Fail() : result.Pass();
    }
}

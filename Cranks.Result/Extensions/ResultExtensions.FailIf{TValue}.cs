namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new instance of <see cref="Failed{TValue}"/> if the <paramref name="condition"/> is true.
        /// Otherwise a new instance of <see cref="Passed{TValue}"/> is returned.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="condition">If true creates <see cref="Failed{TValue}"/>, otherwise <see cref="Passed{TValue}"/>.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> FailIf<TValue>(this IResult<TValue> result, bool condition) => condition ? result.Fail() : result.Pass();

        /// <summary>
        /// Creates a new instance of <see cref="Failed{TValue}"/> if the <paramref name="condition"/> is true.
        /// Otherwise a new instance of <see cref="Passed{TValue}"/> is returned.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="condition">If true creates <see cref="Failed{TValue}"/>, otherwise <see cref="Passed{TValue}"/>.</param>
        /// <param name="value">Value for the <see cref="Passed{TValue}"/> if <paramref name="condition"/> is false.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> FailIf<TValue>(this IResult<TValue> result, bool condition, TValue value) => condition ? result.Fail() : result.Pass(value);
    }
}

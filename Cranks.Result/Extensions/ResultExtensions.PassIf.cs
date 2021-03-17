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

        /// <summary>
        /// Creates a new instance of <see cref="Passed{TValue}"/>, if the <paramref name="condition"/> is true and <paramref name="result"/> is not already <see cref="Failed{TValue}"/>.
        /// Otherwise a new instance of <see cref="Failed{TValue}"/> is created.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="condition">If true creates <see cref="Passed{TValue}"/>, otherwise <see cref="Failed{TValue}"/>.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> PassIf<TValue>(this IResult<TValue> result, bool condition) => condition ? result.Pass() : result.Fail();

        /// <summary>
        /// Creates a new instance of <see cref="Passed{TValue}"/>, if the <paramref name="condition"/> is true and <paramref name="result"/> is not already <see cref="Failed{TValue}"/>.
        /// Otherwise a new instance of <see cref="Failed{TValue}"/> is created.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="condition">If true creates <see cref="Passed{TValue}"/>, otherwise <see cref="Failed{TValue}"/>.</param>
        /// <param name="value">Value for the <see cref="Passed{TValue}"/> if <paramref name="condition"/> is true.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> PassIf<TValue>(this IResult<TValue> result, bool condition, TValue value) => condition ? result.Pass(value) : result.Fail();
    }
}

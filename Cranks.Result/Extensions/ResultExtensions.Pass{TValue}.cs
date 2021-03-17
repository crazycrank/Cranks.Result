namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new instance of <see cref="Passed{Passed}"/>. If <paramref name="result"/> is already <see cref="Failed{Passed}"/>,
        /// a new instance of <see cref="Failed{Passed}"/> is created.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> Pass<TValue>(this IResult<TValue> result)
            => result switch
               {
                   Passed<TValue> passed => new Passed<TValue>(passed.ValueInternal, passed.Message, passed.Causes),
                   Failed => new Failed<TValue>(result.Message, result.Causes),
               };

        /// <summary>
        /// Creates a new instance of <see cref="Passed{Passed}"/>. If <paramref name="result"/> is already <see cref="Failed{Passed}"/>,
        /// a new instance of <see cref="Failed{Passed}"/> is created.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="value">The value of the newly created <see cref="Passed{Passed}"/> instance.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> Pass<TValue>(this IResult<TValue> result, TValue value)
            => result switch
               {
                   Passed<TValue> passed => new Passed<TValue>(value, passed.Message, passed.Causes),
                   Failed => new Failed<TValue>(result.Message, result.Causes),
               };
    }
}

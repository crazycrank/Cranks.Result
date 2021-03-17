namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new <see cref="IResult{TValue}"/> with <see cref="IResult{TValue}.Value"/> set to <paramref name="value"/>.
        /// If <see cref="IResult{TValue}"/> is of type <see cref="Failed{TValue}"/>, the value gets dropped.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="value">The new value of <see cref="IResult{TValue}"/>.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult<TValue> WithValue<TValue>(this IResult result, TValue value)
            => result switch
               {
                   Failed => new Failed<TValue>(result.Message, result.Causes),
                   Passed => new Passed<TValue>(value, result.Message, result.Causes),
               };
    }
}

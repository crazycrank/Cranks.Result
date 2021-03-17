namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new <see cref="IResult{TValue}"/> with <see cref="IResult{TValue}.Message"/> set to <paramref name="message"/>.
        /// </summary>
        /// <param name="result">The source <see cref="IResult{TValue}"/> object the returned object is based on.</param>
        /// <param name="message">The new message of <see cref="IResult{TValue}"/>.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult"/> with the requested modifications.</returns>
        public static IResult<TValue> WithMessage<TValue>(this IResult<TValue> result, string message)
            => result switch
               {
                   Failed => new Failed<TValue>(message, result.Causes),
                   Passed<TValue> passed => new Passed<TValue>(passed.Value, message, result.Causes),
               };
    }
}

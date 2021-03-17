using System;

namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Creates a new <see cref="IResult{TValue}"/> with <see cref="IResult{TValue}.Value"/> set to <paramref name="value"/>, if <paramref name="condition"/> is true.
        /// If <paramref name="result"/> is of type <see cref="Failed"/>, the value gets dropped.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="condition"><paramref name="value"/> is only added to the returned object if this is true.</param>
        /// <param name="value">The new value of <see cref="IResult{TValue}"/> if <paramref name="condition"/> is true.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithValueIf<TValue>(this IResult result, bool condition, TValue value)
            => result switch
               {
                   Failed => new Failed<TValue>(result.Message, result.Causes),
                   Passed<TValue> passed => condition ? new Passed<TValue>(value, result.Message, result.Causes) : new Passed<TValue>(passed.Value, result.Message, result.Causes),
                   Passed => condition ? new Passed<TValue>(value, result.Message, result.Causes) : new Passed<TValue>(default!, result.Message, result.Causes),
               };

        /// <summary>
        /// Creates a new <see cref="IResult{TValue}"/> with <see cref="IResult{TValue}.Value"/> set to <paramref name="value"/>, if <paramref name="condition"/> is true.
        /// If <paramref name="result"/> is of type <see cref="Failed"/>, the value gets dropped.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="condition"><paramref name="value"/> is only added to the returned object if this is true. Otherwise <paramref name="orValue"/> is added.</param>
        /// <param name="value">The new value of <see cref="IResult{TValue}"/> if <paramref name="condition"/> is true.</param>
        /// <param name="orValue">The new value of <see cref="IResult{TValue}"/> if <paramref name="condition"/> is false.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithValueIf<TValue>(this IResult result, bool condition, TValue value, TValue orValue)
            => result switch
               {
                   Failed => new Failed<TValue>(result.Message, result.Causes),
                   Passed => condition ? new Passed<TValue>(value, result.Message, result.Causes) : new Passed<TValue>(orValue, result.Message, result.Causes),
               };

        /// <summary>
        /// Creates a new <see cref="IResult{TValue}"/> with <see cref="IResult{TValue}.Value"/> set to <paramref name="value"/>, if <paramref name="condition"/> is true.
        /// If <paramref name="result"/> is of type <see cref="Failed"/>, the value gets dropped.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="condition"><paramref name="valueFactory"/> is only added to the returned object if this is true.</param>
        /// <param name="valueFactory">Factory method for the value of <see cref="IResult{TValue}"/> if <paramref name="condition"/> is true.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithValueIf<TValue>(this IResult result, bool condition, Func<TValue> valueFactory)
            => result switch
            {
                Failed => new Failed<TValue>(result.Message, result.Causes),
                Passed<TValue> passed => condition ? new Passed<TValue>(valueFactory(), result.Message, result.Causes) : new Passed<TValue>(passed.Value, result.Message, result.Causes),
                Passed => condition ? new Passed<TValue>(valueFactory(), result.Message, result.Causes) : new Passed<TValue>(default!, result.Message, result.Causes),
            };

        /// <summary>
        /// Creates a new <see cref="IResult{TValue}"/> with <see cref="IResult{TValue}.Value"/> set to <paramref name="value"/>, if <paramref name="condition"/> is true.
        /// If <paramref name="result"/> is of type <see cref="Failed"/>, the value gets dropped.
        /// </summary>
        /// <param name="result">The source <see cref="IResult"/> object the returned object is based on.</param>
        /// <param name="condition"><paramref name="valueFactory"/> is only added to the returned object if this is true. Otherwise <paramref name="orValueFactory"/> is added.</param>
        /// <param name="valueFactory">Factory method for the value of <see cref="IResult{TValue}"/> if <paramref name="condition"/> is true.</param>
        /// <param name="orValueFactory">Factory method for the value of <see cref="IResult{TValue}"/> if <paramref name="condition"/> is false.</param>
        /// <typeparam name="TValue">The <see cref="IResult{TValue}"/>s underlying result type.</typeparam>
        /// <returns>A new instance of <see cref="IResult{TValue}"/> with the requested modifications.</returns>
        public static IResult<TValue> WithValueIf<TValue>(this IResult result, bool condition, Func<TValue> valueFactory, Func<TValue> orValueFactory)
            => result switch
            {
                Failed => new Failed<TValue>(result.Message, result.Causes),
                Passed => condition ? new Passed<TValue>(valueFactory(), result.Message, result.Causes) : new Passed<TValue>(orValueFactory(), result.Message, result.Causes),
            };
    }
}

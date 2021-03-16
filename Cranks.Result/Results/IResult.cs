namespace Cranks.Result
{
    /// <summary>
    /// The base IResult interface to indicate Pass/Fail for operations that do not return a value.
    /// </summary>
    /// <remarks>
    /// Do not derive from <see cref="IResult"/>. This is not supported and can lead to unexpected behavior.
    /// </remarks>
    public interface IResult : IReason
    {
        /// <summary>
        /// Gets a value indicating whether the <see cref="IResult"/> has passed.
        /// </summary>
        public bool HasPassed { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="IResult"/> has failed.
        /// </summary>
        public bool HasFailed { get; }
    }

    /// <summary>
    /// The base IResult interface to indicate Pass/Fail for operations that return a value.
    /// </summary>
    /// <remarks>
    /// Do not derive from <see cref="IResult{TValue}"/>. This is not supported and can lead to unexpected behavior.
    /// </remarks>
    /// <typeparam name="TValue">The results underlying value type.</typeparam>
    public interface IResult<out TValue> : IResult
    {
        /// <summary>
        /// Gets the results value.
        /// </summary>
        /// <remarks>Do not access value on unchecked <see cref="IResult{TValue}"/>s, as this getter throws when it is <see cref="Failed"/>.</remarks>
        public TValue Value { get; }
    }
}

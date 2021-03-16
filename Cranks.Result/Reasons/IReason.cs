namespace Cranks.Result
{
    /// <summary>
    /// The base interface for all reason type objects.
    /// </summary>
    /// <remarks>
    /// Should not be inherited directly. Derive from <see cref="Error"/>, <see cref="Success"/> or <see cref="UnexpectedError"/> instead.
    /// </remarks>
    public interface IReason
    {
        /// <summary>
        /// Gets the reasons message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the reasons underlying causes.
        /// </summary>
        public ReasonCollection Reasons { get; }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cranks.Result
{
    /// <summary>
    /// A basic error record to encapsulate an otherwise untyped error. For more specific Error types it is recommended to derive from this record.<br/>
    /// Errors are fully immutable. Manipulate it using the With* methods in <see cref="ResultExtensions"/>.<br />
    /// Can be implicitly casted from string.
    /// </summary>
    public record Error
        : IReason
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> record with the provided <paramref name="reasons"/>.
        /// </summary>
        /// <param name="reasons">The Errors <see cref="Reasons"/>.</param>
        public Error(params IReason[] reasons)
            : this(reasons.AsEnumerable())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> record with the provided <paramref name="reasons"/>.
        /// </summary>
        /// <param name="reasons">The Errors <see cref="Reasons"/>.</param>
        public Error(IEnumerable<IReason> reasons)
            : this(string.Empty, reasons)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> record with the provided <paramref name="message"/> and <paramref name="reasons"/>.
        /// </summary>
        /// <param name="message">The Errors <see cref="Message"/>.</param>
        /// <param name="reasons">The Errors <see cref="Reasons"/>.</param>
        public Error(string message, params IReason[] reasons)
            : this(message, reasons.AsEnumerable())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> record with the provided <paramref name="message"/> and <paramref name="reasons"/>.
        /// </summary>
        /// <param name="message">The Errors <see cref="Message"/>.</param>
        /// <param name="reasons">The Errors <see cref="Reasons"/>.</param>
        public Error(string message, IEnumerable<IReason> reasons)
        {
            Message = message;
            Reasons = new ReasonCollection(reasons);
        }

        /// <inheritdoc />
        public string Message { get; }

        /// <inheritdoc />
        public ReasonCollection Reasons { get; }

        public static implicit operator Error(string message) => new(message);

        protected virtual bool PrintMembers(StringBuilder builder)
        {
            // custom PrintMembers implementation to not print Reasons when Reasons is empty for better readability
            builder.Append(nameof(Message));
            builder.Append(" = ");
            builder.Append(Message);

            if (Reasons.Any())
            {
                builder.Append(", ");

                builder.Append(nameof(Reasons));
                builder.Append(" = ");
                builder.Append(Reasons);
            }

            return true;
        }
    }
}

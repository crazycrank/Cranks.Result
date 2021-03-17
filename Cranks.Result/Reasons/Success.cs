using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cranks.Result
{
    /// <summary>
    /// A basic success record to encapsulate an otherwise untyped success. For more specific Success types it is recommended to derive from this record.<br/>
    /// Successes are fully immutable. Manipulate it using the With* methods in <see cref="ResultExtensions"/>.<br />
    /// Can be implicitly casted from string.
    /// </summary>
    public record Success
        : IReason
    {
        public Success()
            : this(Enumerable.Empty<IReason>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Success"/> record with the provided <paramref name="reasons"/>.
        /// </summary>
        /// <param name="reasons">The Successes <see cref="Causes"/>.</param>
        public Success(params IReason[] reasons)
            : this(reasons.AsEnumerable())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Success"/> record with the provided <paramref name="reasons"/>.
        /// </summary>
        /// <param name="reasons">The Successes <see cref="Causes"/>.</param>
        public Success(IEnumerable<IReason> reasons)
            : this(string.Empty, reasons)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Success"/> record with the provided <paramref name="message"/> and <paramref name="reasons"/>.
        /// </summary>
        /// <param name="message">The Successes <see cref="Message"/>.</param>
        /// <param name="reasons">The Successes <see cref="Causes"/>.</param>
        public Success(string message, params IReason[] reasons)
            : this(message, reasons.AsEnumerable())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Success"/> record with the provided <paramref name="message"/> and <paramref name="reasons"/>.
        /// </summary>
        /// <param name="message">The Successes <see cref="Message"/>.</param>
        /// <param name="reasons">The Successes <see cref="Causes"/>.</param>
        public Success(string message, IEnumerable<IReason> reasons)
        {
            Message = message;
            Causes = new ReasonCollection(reasons);
        }

        /// <inheritdoc />
        public string Message { get; }

        /// <inheritdoc />
        public ReasonCollection Causes { get; }

        public static implicit operator Success(string message) => new(message);

        protected virtual bool PrintMembers(StringBuilder builder)
        {
            // custom PrintMembers implementation to not print Causes when Causes is empty for better readability
            builder.Append(nameof(Message));
            builder.Append(" = ");
            builder.Append(Message);

            if (Causes.Any())
            {
                builder.Append(", ");

                builder.Append(nameof(Causes));
                builder.Append(" = ");
                builder.Append(Causes);
            }

            return true;
        }
    }
}

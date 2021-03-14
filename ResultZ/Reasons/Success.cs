using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResultZ
{
    public record Success
        : IReason
    {
        public Success(params IReason[] reasons)
            : this(reasons.AsEnumerable())
        {
        }

        public Success(IEnumerable<IReason> reasons)
            : this(string.Empty, reasons)
        {
        }

        public Success(string message, params IReason[] reasons)
            : this(message, reasons.AsEnumerable())
        {
        }

        public Success(string message, IEnumerable<IReason> reasons)
        {
            Message = message;
            Reasons = new ReasonCollection(reasons);
        }

        public string Message { get; }

        public ReasonCollection Reasons { get; }

        public static implicit operator Success(string message) => new(message);

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

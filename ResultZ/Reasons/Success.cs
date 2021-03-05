using System.Collections.Generic;
using System.Linq;

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
    }
}

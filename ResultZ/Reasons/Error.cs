using System.Collections.Generic;
using System.Linq;

namespace ResultZ
{
    public record Error
        : IReason
    {
        public Error(params IReason[] reasons)
            : this(reasons.AsEnumerable())
        {
        }

        public Error(IEnumerable<IReason> reasons)
            : this(string.Empty, reasons)
        {
        }

        public Error(string message, params IReason[] reasons)
            : this(message, reasons.AsEnumerable())
        {
        }

        public Error(string message, IEnumerable<IReason> reasons)
        {
            Message = message;
            Reasons = new ReasonCollection(reasons);
        }

        public string Message { get; }

        public ReasonCollection Reasons { get; }

        public static implicit operator Error(string message) => new(message);
    }
}

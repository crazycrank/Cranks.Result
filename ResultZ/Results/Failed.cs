using System.Collections.Generic;
using System.Linq;

namespace ResultZ
{
    public record Failed : Error, IResult
    {
        internal Failed(params IReason[] reasons)
            : this(reasons.AsEnumerable())
        {
        }

        internal Failed(IEnumerable<IReason> reasons)
            : this(string.Empty, reasons)
        {
        }

        internal Failed(string message, params IReason[] reasons)
            : this(message, reasons.AsEnumerable())
        {
        }

        internal Failed(string message, IEnumerable<IReason> reasons)
            : base(message, reasons)
        {
        }

        public bool HasFailed => true;
        public bool HasPassed => false;
    }

    public record Failed<TValue> : Failed, IResult<TValue>
    {
        internal Failed(params IReason[] reasons)
            : this(reasons.AsEnumerable())
        {
        }

        internal Failed(IEnumerable<IReason> reasons)
            : this(string.Empty, reasons)
        {
        }

        internal Failed(string message, params IReason[] reasons)
            : this(message, reasons.AsEnumerable())
        {
        }

        internal Failed(string message, IEnumerable<IReason> reasons)
            : base(message, reasons)
        {
        }

        public TValue? Value => default;
    }
}

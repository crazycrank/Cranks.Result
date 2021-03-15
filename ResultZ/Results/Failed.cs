using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        protected override bool PrintMembers(StringBuilder builder)
        {
            // custom PrintMembers implementation to not print HasPassed and HasFailed properties
            base.PrintMembers(builder);
            return true;
        }
    }

    // TODO does it make sense to have a failed with a value?
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

        public TValue Value => throw new ResultException("Do not access Value of Failed<TValue>. Check for Passed before accessing.", this);

        protected override bool PrintMembers(StringBuilder builder)
        {
            // custom PrintMembers implementation to not print Value in case of Failed (as accessing Value of Failed always throws)
            base.PrintMembers(builder);
            return true;
        }
    }
}

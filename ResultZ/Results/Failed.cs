using System;
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

        // TODO: default or throw?
        // case for default: accessing doesn't throw.
        // case for throw: should never be accessed unchecked. basically illegal operation
        public TValue Value => throw new InvalidOperationException("Do not access Value of Failed<TValue>. Check for Passed before accessing.");
    }
}

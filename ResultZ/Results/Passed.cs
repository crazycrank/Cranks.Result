using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResultZ
{
    public record Passed : Success, IResult
    {
        internal Passed(params IReason[] reasons)
            : this(reasons.AsEnumerable())
        {
        }

        internal Passed(IEnumerable<IReason> reasons)
            : this(string.Empty, reasons)
        {
        }

        internal Passed(string message, params IReason[] reasons)
            : this(message, reasons.AsEnumerable())
        {
        }

        internal Passed(string message, IEnumerable<IReason> reasons)
            : base(message, reasons)
        {
            if (reasons.Any(r => r is Error))
            {
                throw new ResultException("Cannot create Success containing an Error");
            }
        }

        public bool HasFailed => false;
        public bool HasPassed => true;

        protected override bool PrintMembers(StringBuilder builder)
        {
            // custom PrintMembers implementation to not print HasPassed and HasFailed properties
            base.PrintMembers(builder);
            return true;
        }
    }

    public record Passed<TValue> : Passed, IResult<TValue>
    {
        internal Passed(TValue value, params IReason[] reasons)
            : this(value, reasons.AsEnumerable())
        {
        }

        internal Passed(TValue value, IEnumerable<IReason> reasons)
            : this(value, string.Empty, reasons)
        {
        }

        internal Passed(TValue value, string message, params IReason[] reasons)
            : this(value, message, reasons.AsEnumerable())
        {
        }

        internal Passed(TValue value, string message, IEnumerable<IReason> reasons)
            : base(message, reasons)
        {
            Value = value;
        }

        public TValue Value { get; }
    }
}

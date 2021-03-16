using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Cranks.Result
{
    /// <summary>
    /// Indicates a failed operation.
    /// </summary>
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

        /// <inheritdoc />
        public bool HasFailed => true;

        /// <inheritdoc />
        public bool HasPassed => false;

        protected override bool PrintMembers(StringBuilder builder)
        {
            // custom PrintMembers implementation to not print HasPassed and HasFailed properties
            base.PrintMembers(builder);
            return true;
        }
    }

    /// <summary>
    /// Indicates a failed operation with value.
    /// </summary>
    /// <typeparam name="TValue">The results underlying value type.</typeparam>
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

        /// <summary>
        /// Always throws. Accessing a <see cref="Failed{TValue}"/>s error is an illegal operation.
        /// </summary>
        public TValue Value
        {
            [DoesNotReturn]
            get => throw new ResultException("Do not access Value of Failed<TValue>. Check for Passed before accessing.", this);
        }

        protected override bool PrintMembers(StringBuilder builder)
        {
            // custom PrintMembers implementation to not print Value in case of Failed (as accessing Value of Failed always throws)
            base.PrintMembers(builder);
            return true;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cranks.Result
{
    /// <summary>
    /// Indicates a passed operation.
    /// </summary>
    public record Passed : Success, IResult
    {
        internal Passed(params IReason[] causes)
            : this(causes.AsEnumerable())
        {
        }

        internal Passed(IEnumerable<IReason> causes)
            : this(string.Empty, causes)
        {
        }

        internal Passed(string message, params IReason[] causes)
            : this(message, causes.AsEnumerable())
        {
        }

        internal Passed(string message, IEnumerable<IReason> causes)
            : base(message, causes)
        {
            if (causes.Any(r => r is Error))
            {
                throw new ResultException("Cannot create Success containing an Error");
            }
        }

        /// <inheritdoc />
        public bool HasFailed => false;

        /// <inheritdoc />
        public bool HasPassed => true;

        protected override bool PrintMembers(StringBuilder builder)
        {
            // custom PrintMembers implementation to not print HasPassed and HasFailed properties
            base.PrintMembers(builder);
            return true;
        }
    }

    /// <summary>
    /// Indicates a passed operation with value.
    /// </summary>
    /// <typeparam name="TValue">The results underlying value type.</typeparam>
    public record Passed<TValue> : Passed, IResult<TValue>
    {
        private readonly TValue? _value;

        internal Passed()
        {
        }

        internal Passed(TValue value, params IReason[] causes)
            : this(value, causes.AsEnumerable())
        {
        }

        internal Passed(TValue value, IEnumerable<IReason> causes)
            : this(value, string.Empty, causes)
        {
        }

        internal Passed(TValue value, string message, params IReason[] causes)
            : this(value, message, causes.AsEnumerable())
        {
        }

        internal Passed(TValue value, string message, IEnumerable<IReason> causes)
            : base(message, causes)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the value of the passed operation.
        /// </summary>
        public TValue Value => _value ?? throw new ResultException("Value must be set before accessing it"); // TODO: right now this is not throwing for value types. Not sure if this is a problem.

        internal TValue ValueInternal => _value!; // not true, but how it is intended to be used

        protected override bool PrintMembers(StringBuilder builder)
        {
            if (base.PrintMembers(builder))
            {
                builder.Append(", ");
            }

            builder.Append(nameof(Value));
            builder.Append(" = ");
            builder.Append(_value?.ToString() ?? "null"); // allow printing even when _value is null (wrongful usage)

            return true;
        }
    }
}

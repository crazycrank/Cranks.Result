using System;
using System.Collections.Generic;
using System.Linq;

namespace ResultZ
{
    public interface IResult : IReason
    {
    }

    public interface IResult<out TValue> : IResult
    {
        public TValue? Value { get; }
    }

    public record Failed : Error, IResult
    {
        internal Failed(params IReason[] reasons)
            : this(reasons.AsEnumerable())
        {
        }

        internal Failed(IEnumerable<IReason> reasons)
            : base(Reasons: new ReasonCollection(reasons))
        {
        }
    }

    public record Failed<TValue> : Failed, IResult<TValue>
    {
        internal Failed(params IReason[] reasons)
            : base(reasons)
        {
        }

        internal Failed(IEnumerable<IReason> reasons)
            : base(reasons)
        {
        }

        public TValue? Value => default;
    }

    public record Passed : Success, IResult
    {
        internal Passed(params IReason[] reasons)
            : this(reasons.AsEnumerable())
        {
        }

        internal Passed(IEnumerable<IReason> reasons)
            : base(Reasons: new ReasonCollection(reasons))
        {
            if (reasons.Any(r => r is Error))
            {
                throw new InvalidOperationException("Cannot create Success containing an Error");
            }
        }
    }

    public record Passed<TValue> : Passed, IResult<TValue>
    {
        internal Passed(TValue value, params IReason[] reasons)
            : base(reasons)
        {
            Value = value;
        }

        internal Passed(TValue value, IEnumerable<IReason> reasons)
            : base(reasons)
        {
            Value = value;
        }

        public TValue Value { get; }
    }
}

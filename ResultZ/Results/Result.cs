using System;
using System.Collections.Generic;
using System.Linq;

using ResultZ.Reasons;

namespace ResultZ.Results
{
    public interface IResult
    {
        public ReasonCollection Reasons { get; }
    }

    public interface IResult<out TValue> : IResult
    {
        public TValue? Value { get; }
    }

    public record Failure
        : IResult
    {
        internal Failure(IResult result, params Reason[] reasons)
            : this(result, reasons.AsEnumerable())
        {
        }

        internal Failure(IResult result, IEnumerable<Reason> reasons)
            : this(result.Reasons.Concat(reasons))
        {
        }

        internal Failure(params Reason[] reasons)
            : this(reasons.AsEnumerable())
        {
        }

        internal Failure(IEnumerable<Reason> reasons)
        {
            Reasons = new ReasonCollection(reasons);
        }

        public ReasonCollection Reasons { get; }
    }

    public record Failure<TValue>
        : Failure, IResult<TValue>
    {
        internal Failure(IResult<TValue> result, params Reason[] reasons)
            : base(result, reasons)
        {
        }

        internal Failure(IResult<TValue> result, IEnumerable<Reason> reasons)
            : base(result, reasons)
        {
        }

        internal Failure(params Reason[] reasons)
            : base(reasons)
        {
        }

        internal Failure(IEnumerable<Reason> reasons)
            : base(reasons)
        {
        }

        public TValue? Value => default;
    }

    public record Successful
        : IResult
    {
        internal Successful(IResult result, params Reason[] reasons)
            : this(result, reasons.AsEnumerable())
        {
        }

        internal Successful(IResult result, IEnumerable<Reason> reasons)
            : this(result.Reasons.Concat(reasons))
        {
        }

        internal Successful(params Reason[] reasons)
            : this(reasons.AsEnumerable())
        {
        }

        internal Successful(IEnumerable<Reason> reasons)
        {
            if (reasons.Any(r => r is Error))
            {
                throw new InvalidOperationException("Cannot create Success with still Error reason");
            }

            Reasons = new ReasonCollection(reasons);
        }

        public ReasonCollection Reasons { get; } = new();
    }

    public record Successful<TValue>
        : Successful, IResult<TValue>
    {
        internal Successful(Successful<TValue> result, params Reason[] reasons)
            : base(result, reasons)
        {
            Value = result.Value;
        }

        internal Successful(Successful<TValue> result, IEnumerable<Reason> reasons)
            : base(result, reasons)
        {
            Value = result.Value;
        }

        internal Successful(TValue value, params Reason[] reasons)
            : base(reasons)
        {
            Value = value;
        }

        internal Successful(TValue value, IEnumerable<Reason> reasons)
            : base(reasons)
        {
            Value = value;
        }

        public TValue Value { get; }
    }

    public static class Result
    {
        public static IResult Successful() => new Successful();
        public static IResult Successful(params Reason[] reasons) => new Successful(reasons);
        public static IResult Successful(IEnumerable<Reason> reasons) => new Successful(reasons);

        public static IResult<TValue> Successful<TValue>(TValue value) => new Successful<TValue>(value);
        public static IResult<TValue> Successful<TValue>(TValue value, params Reason[] reasons) => new Successful<TValue>(value, reasons);
        public static IResult<TValue> Successful<TValue>(TValue value, IEnumerable<Reason> reasons) => new Successful<TValue>(value, reasons);

        public static IResult Failure() => new Failure();
        public static IResult Failure(params Reason[] reasons) => new Failure(reasons);
        public static IResult Failure(IEnumerable<Reason> reasons) => new Failure(reasons);
        public static IResult Failure(IResult cause, IEnumerable<Reason> reasons) => new Failure(reasons);

        public static IResult<TValue> Failure<TValue>() => new Failure<TValue>();
        public static IResult<TValue> Failure<TValue>(params Reason[] reasons) => new Failure<TValue>(reasons);
        public static IResult<TValue> Failure<TValue>(IEnumerable<Reason> reasons) => new Failure<TValue>(reasons);

        public static IResult From(IResult result)
        {
            return result switch
                   {
                       Failure _ => new Failure(new Error(Causes: result.Reasons)),
                       Successful _ => new Successful(new Success(Causes: result.Reasons)),
                       _ => throw new ArgumentOutOfRangeException(nameof(result)),
                   };
        }

        public static IResult<TTargetValue> From<TTargetValue, TSourceValue>(IResult<TSourceValue> result)
        {
            // TODO something doesn't feel quite right with the Result Constructors
            return result switch
                   {
                       Failure<TTargetValue> => new Failure<TTargetValue>(new Error(Causes: result.Reasons)),
                       Successful<TTargetValue> success => new Successful<TTargetValue>(success, new Success(Causes: result.Reasons)),
                       _ => throw new ArgumentOutOfRangeException(nameof(result)),
                   };
        }
    }
}

using System.Diagnostics.CodeAnalysis;

namespace ResultZ
{
    public interface IResult : IReason
    {
        public bool HasPassed { get; }

        public bool HasFailed { get; }
    }

    public interface IResult<out TValue> : IResult
    {
        public TValue? Value { get; }
    }
}

namespace ResultZ
{
    public interface IResult : IReason
    {
    }

    public interface IResult<out TValue> : IResult
    {
        public TValue? Value { get; }
    }
}

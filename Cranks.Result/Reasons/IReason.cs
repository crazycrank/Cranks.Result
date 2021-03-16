namespace Cranks.Result
{
    public interface IReason
    {
        public string Message { get; }

        public ReasonCollection Reasons { get; }
    }
}

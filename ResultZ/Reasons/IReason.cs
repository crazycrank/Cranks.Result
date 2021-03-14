namespace ResultZ
{
    public interface IReason
    {
        public string Message { get; }

        public ReasonCollection Reasons { get; }
    }
}

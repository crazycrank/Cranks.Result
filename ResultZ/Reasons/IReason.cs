using System;

namespace ResultZ.Reasons
{
    public interface IReason
    {
        public string Message { get; }

        public ReasonCollection Reasons { get; }
    }

    public record Error(string Message = "", ReasonCollection? Reasons = null)
        : IReason
    {
        public string Message { get; } = Message;

        public ReasonCollection Reasons { get; } = Reasons ?? new();

        public static implicit operator Error(string message) => new(message);
    }

    public record UnexpectedError(Exception Exception, ReasonCollection? Reasons = null)
        : Error(Exception.Message, Reasons);

    public record Success(string Message = "", ReasonCollection? Reasons = null)
        : IReason
    {
        public string Message { get; } = Message;

        public ReasonCollection Reasons { get; } = Reasons ?? new();

        public static implicit operator Success(string message) => new(message);
    }
}

using System;

namespace ResultZ.Reasons
{
    public abstract record Reason(string Message = "", ReasonCollection? Causes = null)
    {
        public string Message { get; } = Message;

        public ReasonCollection Causes { get; } = Causes ?? new();
    }

    public record Error(string Message = "", ReasonCollection? Causes = null)
        : Reason(Message, Causes);

    public record UnexpectedError(Exception Exception, ReasonCollection? Causes = null)
        : Error(Exception.Message, Causes);

    public record Success(string Message = "", ReasonCollection? Causes = null)
        : Reason(Message, Causes);
}

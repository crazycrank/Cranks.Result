namespace Cranks.Result
{
    /// <summary>
    /// A basic error record to encapsulate errors containing an exception.<br/>
    /// Errors are fully immutable. Manipulate it using the With* methods in <see cref="ResultExtensions"/>.<br />
    /// Can be implicitly casted from string.
    public record UnexpectedError(System.Exception Exception, ReasonCollection? Reasons = null)
        : Error(Exception.Message, Reasons ?? new());
}

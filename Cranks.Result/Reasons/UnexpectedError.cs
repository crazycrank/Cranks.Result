namespace Cranks.Result
{
    public record UnexpectedError(System.Exception Exception, ReasonCollection? Reasons = null)
        : Error(Exception.Message, Reasons ?? new());
}

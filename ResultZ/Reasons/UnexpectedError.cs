using System;

namespace ResultZ
{
    public record UnexpectedError(Exception Exception, ReasonCollection? Reasons = null)
        : Error(Exception.Message, Reasons ?? new());
}

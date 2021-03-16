using System;

namespace Cranks.Result
{
    public sealed class ResultException : InvalidOperationException
    {
        public ResultException(string message)
            : base(message)
        {
        }

        public ResultException(string message, IReason reason)
            : this($"{message}{Environment.NewLine}{reason}")
        {
            Reason = reason;
        }

        public IReason? Reason { get; }
    }
}

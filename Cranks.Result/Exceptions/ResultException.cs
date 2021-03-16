using System;

namespace Cranks.Result
{
    /// <summary>
    /// Standard exception of the Cranks.Result library.
    /// </summary>
    public sealed class ResultException : InvalidOperationException
    {
        internal ResultException(string message)
            : base(message)
        {
        }

        internal ResultException(string message, IReason reason)
            : this($"{message}{Environment.NewLine}{reason}")
        {
            Reason = reason;
        }

        /// <summary>
        /// If there is a <see cref="IReason"/> attached to the exception it can be accessed here.
        /// </summary>
        public IReason? Reason { get; }
    }
}

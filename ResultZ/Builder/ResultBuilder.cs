using System;
using System.Collections.Generic;
using System.Linq;

namespace ResultZ
{
    public sealed class ResultBuilder
    {
        private readonly List<(Func<bool> Predicate, Func<IReason> ReasonFunc)> _reasons = new();

        private string? _message;
        private bool _hasErrors;

        public ResultBuilder WithMessage(string message)
        {
            _message = message;
            return this;
        }

        public ResultBuilder WithSuccessIf(bool predicate, Success success) => WithSuccessIf(() => predicate, () => success);
        public ResultBuilder WithSuccessIf(Func<bool> predicate, Success success) => WithSuccessIf(predicate, () => success);
        public ResultBuilder WithSuccessIf(bool predicate, Func<Success> success) => WithSuccessIf(() => predicate, success);
        public ResultBuilder WithSuccessIf(Func<bool> predicate, Func<Success> success)
        {
            _reasons.Add((predicate, success));
            return this;
        }

        public ResultBuilder WithErrorIf(bool predicate, Error error) => WithErrorIf(() => predicate, () => error);
        public ResultBuilder WithErrorIf(Func<bool> predicate, Error error) => WithErrorIf(predicate, () => error);
        public ResultBuilder WithErrorIf(bool predicate, Func<Error> error) => WithErrorIf(() => predicate, error);
        public ResultBuilder WithErrorIf(Func<bool> predicate, Func<Error> error)
        {
            _hasErrors = true;
            _reasons.Add((predicate, error));
            return this;
        }

        public IResult Build()
        {
            var result = _hasErrors
                             ? Result.Fail()
                             : Result.Pass();

            if (_message is not null)
            {
                result = result.WithMessage(_message);
            }

            return result.WithReason(_reasons.Where(r => r.Predicate()).Select(r => r.ReasonFunc()));
        }

        // TODO some implicit conversion to a IResult would be nice, but conversions to interfaces are not allowed...
    }
}

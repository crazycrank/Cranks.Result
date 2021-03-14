using System;
using System.Collections.Generic;

namespace ResultZ
{
    public sealed class ResultBuilder
    {
        private readonly List<IReason> _reasons = new();

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
            if (predicate())
            {
                _reasons.Add(success());
            }

            return this;
        }

        public ResultBuilder WithErrorIf(bool predicate, Error error) => WithErrorIf(() => predicate, () => error);
        public ResultBuilder WithErrorIf(Func<bool> predicate, Error error) => WithErrorIf(predicate, () => error);
        public ResultBuilder WithErrorIf(bool predicate, Func<Error> error) => WithErrorIf(() => predicate, error);
        public ResultBuilder WithErrorIf(Func<bool> predicate, Func<Error> error)
        {
            if (predicate())
            {
                _hasErrors = true;
                _reasons.Add(error());
            }

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

            return result.WithReason(_reasons);
        }

        // TODO some implicit conversion to a IResult would be nice, but conversions to interfaces are not allowed...
    }
}

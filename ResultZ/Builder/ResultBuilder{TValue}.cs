using System;
using System.Collections.Generic;
using System.Linq;

namespace ResultZ
{
    public sealed class ResultBuilder<TValue>
    {
        private readonly List<(Func<bool> Predicate, Func<IReason> ReasonFunc)> _reasons = new();

        private TValue? _value;
        private string? _message;
        private bool _hasErrors;

        public ResultBuilder<TValue> WithValue(TValue value)
        {
            _value = value;
            return this;
        }

        public ResultBuilder<TValue> WithMessage(string message)
        {
            _message = message;
            return this;
        }

        public ResultBuilder<TValue> WithSuccessIf(bool predicate, Success success) => WithSuccessIf(() => predicate, () => success);
        public ResultBuilder<TValue> WithSuccessIf(Func<bool> predicate, Success success) => WithSuccessIf(predicate, () => success);
        public ResultBuilder<TValue> WithSuccessIf(bool predicate, Func<Success> success) => WithSuccessIf(() => predicate, success);
        public ResultBuilder<TValue> WithSuccessIf(Func<bool> predicate, Func<Success> success)
        {
            _reasons.Add((predicate, success));
            return this;
        }

        public ResultBuilder<TValue> WithErrorIf(bool predicate, Error error) => WithErrorIf(() => predicate, () => error);
        public ResultBuilder<TValue> WithErrorIf(Func<bool> predicate, Error error) => WithErrorIf(predicate, () => error);
        public ResultBuilder<TValue> WithErrorIf(bool predicate, Func<Error> error) => WithErrorIf(() => predicate, error);
        public ResultBuilder<TValue> WithErrorIf(Func<bool> predicate, Func<Error> error)
        {
            _hasErrors = true;
            _reasons.Add((predicate, error));
            return this;
        }

        public IResult<TValue> Build()
        {
            if (!_hasErrors && _value is null)
            {
                throw new ResultException("Can not create Passed<TValue> from builder without having a value assigned");
            }

            var result = _hasErrors
                             ? Result.Fail<TValue>()
                             : Result.Pass(_value!);

            if (_message is not null)
            {
                result = result.WithMessage(_message);
            }

            return result.WithReason(_reasons.Where(r => r.Predicate()).Select(r => r.ReasonFunc()));
        }

        // TODO some implicit conversion to a IResult would be nice, but conversions to interfaces are not allowed...
    }
}

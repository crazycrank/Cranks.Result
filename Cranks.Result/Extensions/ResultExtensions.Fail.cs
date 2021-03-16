using System;

namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        public static IResult Fail(this IResult result)
            => HandleGenericVariant(result, nameof(Pass)) switch
            {
                Passed<IResult> { Value: var genericResult } => genericResult,
                Failed => new Failed(result.Message, result.Reasons),
            };

        public static IResult<TValue> Fail<TValue>(this IResult<TValue> result) => new Failed<TValue>(result.Message, result.Reasons);
    }
}

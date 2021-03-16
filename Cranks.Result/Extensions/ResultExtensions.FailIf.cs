using System;

namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        public static IResult FailIf(this IResult result, bool condition) => condition ? result.Fail() : result.Pass();
        public static IResult<TValue> FailIf<TValue>(this IResult<TValue> result, TValue value, bool condition) => condition ? result.Fail() : result.Pass(value);
    }
}

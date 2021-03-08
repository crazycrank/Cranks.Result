using System.Collections.Generic;

namespace ResultZ
{
    public static class Result
    {
        public static IResult Pass() => new Passed();
        public static IResult<TValue> Pass<TValue>(TValue value) => new Passed<TValue>(value);

        public static IResult Fail() => new Failed();
        public static IResult<TValue> Fail<TValue>() => new Failed<TValue>();

        public static IResult PassIf(bool condition) => condition ? new Passed() : new Failed();
        public static IResult<TValue> PassIf<TValue>(TValue value, bool condition) => condition ? new Passed<TValue>(value) : new Failed<TValue>();

        public static IResult FailIf(bool condition) => condition ? new Failed() : new Passed();
        public static IResult<TValue> FailIf<TValue>(TValue value, bool condition) => condition ? new Failed<TValue>() : new Passed<TValue>(value);

        // TODO Empty result?
    }
}

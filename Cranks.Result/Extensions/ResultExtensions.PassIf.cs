namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        public static IResult PassIf(this IResult result, bool condition) => condition ? result.Pass() : result.Fail();

        public static IResult<TValue> PassIf<TValue>(this IResult<TValue> result, TValue value, bool condition) => condition ? result.Pass(value) : result.Fail();
    }
}

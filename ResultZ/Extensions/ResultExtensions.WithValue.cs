namespace ResultZ
{
    public static partial class ResultExtensions
    {
        public static IResult<TValue> WithValue<TValue>(this IResult result, TValue value)
            => result switch
               {
                   Failed => new Failed<TValue>(result.Message, result.Reasons),
                   Passed => new Passed<TValue>(value, result.Message, result.Reasons),
               };
    }
}

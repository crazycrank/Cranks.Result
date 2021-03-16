namespace Cranks.Result
{
    public static partial class ResultExtensions
    {
        public static IResult Pass(this IResult result)
            => HandleGenericVariant(result, nameof(Pass)) switch
            {
                Passed<IResult> { Value: var genericResult } => genericResult,
                Failed => result switch
                          {
                              Passed passed => new Passed(passed.Message, passed.Reasons),
                              Failed => throw new ResultException("Can not convert a Failed into a Passed"),
                          },
            };

        public static IResult<TValue> Pass<TValue>(this IResult<TValue> result, TValue value)
            => result switch
               {
                   Passed<TValue> passed => new Passed<TValue>(value, passed.Message, passed.Reasons),
                   Failed => throw new ResultException("Can not convert a Failed into a Passed"),
               };
    }
}

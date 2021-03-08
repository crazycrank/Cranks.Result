namespace ResultZ
{
    public static partial class ResultExtensions
    {
        public static IResult WithMessage(this IResult result, string message)
        => HandleGenericVariant(result, nameof(WithMessage), message) switch
        {
            Passed<IResult> { Value: not null and var genericResult } => genericResult,
            Failed => result switch
            {
                Failed => new Failed(message, result.Reasons),
                Passed => new Passed(message, result.Reasons),
            },
        };

        public static IResult<TValue> WithMessage<TValue>(this IResult<TValue> result, string message)
            => result switch
               {
                   Failed => new Failed<TValue>(message, result.Reasons),
                   Passed<TValue> passed => new Passed<TValue>(passed.Value, message, result.Reasons),
               };
    }
}

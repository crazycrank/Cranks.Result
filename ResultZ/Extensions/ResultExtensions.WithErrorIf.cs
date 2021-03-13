namespace ResultZ
{
    // TODO: needs overloads that return IResult<TValue> to keep consistency
    public static partial class ResultExtensions
    {
        public static IResult WithErrorIf<TError>(this IResult result, bool condition, TError error)
            where TError : Error
            => condition ? result.WithError(error) : result;

        public static IResult WithErrorIf<TError>(this IResult result, bool condition)
            where TError : Error, new()
            => result.WithErrorIf(condition, new TError());

        // TODO: errors should be created using an action. Otherwise they need to be evaluated before passing, leading to ugly nullability issues (and similiar stuff)
        public static IResult WithErrorIf<TError, TSuccess>(this IResult result,
                                                       bool condition,
                                                       TError error,
                                                       TSuccess orSuccess)
            where TError : Error
            where TSuccess : Success
            => condition ? result.WithError(error) : result.WithSuccess(orSuccess);

        public static IResult WithErrorIf<TError, TSuccess>(this IResult result, bool condition)
            where TError : Error, new()
            where TSuccess : Success, new()
            => result.WithErrorIf(condition, new TError(), new TSuccess());
    }
}

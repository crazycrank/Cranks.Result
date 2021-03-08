namespace ResultZ
{
    public static partial class ResultExtensions
    {
        public static IResult WithSuccessIf<TSuccess>(this IResult result, bool condition, TSuccess success)
            where TSuccess : Success
            => condition ? result.WithSuccess(success) : result;

        public static IResult WithSuccessIf<TSuccess>(this IResult result, bool condition)
            where TSuccess : Success, new()
            => result.WithSuccessIf(condition, new TSuccess());

        public static IResult WithSuccessIf<TSuccess, TError>(this IResult result,
                                                       bool condition,
                                                       TSuccess success,
                                                       TError orError)
            where TSuccess : Success
            where TError : Error
            => condition ? result.WithSuccess(success) : result.WithError(orError);

        public static IResult WithSuccessIf<TSuccess, TError>(this IResult result, bool condition)
            where TSuccess : Success, new()
            where TError : Error, new()
            => result.WithSuccessIf(condition, new TSuccess(), new TError());
    }
}

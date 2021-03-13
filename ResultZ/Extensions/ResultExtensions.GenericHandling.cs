using System;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.Caching.Memory;

namespace ResultZ
{
    public static partial class ResultExtensions
    {
        private static readonly MemoryCache _cache = new(new MemoryCacheOptions());

        internal static IResult<IResult> HandleGenericVariant<TParam>(this IResult result, string methodName, TParam param)
        {
            // dogfooding
            return GetGenericMethod(result.GetType(), methodName) switch
                   {
                       Failed => Result.Fail<IResult>(),
                       Passed<MethodInfo> { Value: var genericMethod } => Result.Pass((IResult)genericMethod.Invoke(null, new object?[] { result, param })!),
                   };
        }

        private static IResult<MethodInfo> GetGenericMethod(Type resultType, string methodName)
        {
            if (!resultType.IsGenericType)
            {
                return Result.Fail<MethodInfo>();
            }

            var resultValueType = resultType.GetGenericArguments().Single();
            var genericResultType = typeof(IResult<>).MakeGenericType(resultValueType);
            if (!resultType.IsAssignableTo(genericResultType))
            {
                return Result.Fail<MethodInfo>();
            }

            var generifiedMethod = _cache.GetOrCreate((resultType, methodName), CreateGenericSingleReasonsMethod);

            return Result.Pass(generifiedMethod);

            MethodInfo CreateGenericSingleReasonsMethod(ICacheEntry entry)
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(60);

                // TODO maybe create a func from the method info
                ////Expression.Lambda<Func<IResult<TValue>, Reason, IResult>()
                var genericMethod = typeof(ResultExtensions).GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                                            .Single(m => m.Name == methodName && m.IsGenericMethod)
                                                            .MakeGenericMethod(resultValueType);
                return genericMethod;
            }
        }
    }
}

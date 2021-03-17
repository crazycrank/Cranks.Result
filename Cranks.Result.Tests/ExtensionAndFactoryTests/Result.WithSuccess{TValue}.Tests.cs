using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultWithSuccessOfTValueTests
    {
        [Fact]
        public void WithSuccess_ResultHasSuccess()
        {
            var result = Result.WithSuccess<int>(new Success());

            result.ShouldBeOfType<Passed<int>>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Success());
        }

        [Fact]
        public void WithSuccessOfTSuccess_InstantiatesTSuccess_ResultHasSuccess()
        {
            var result = Result.WithSuccess<int, Success>();

            result.ShouldBeOfType<Passed<int>>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Success());
        }

        [Fact]
        public void WithSuccess_OnFailed_StaysFailed()
        {
            var result = Result.Fail<int>()
                               .WithSuccess<Success>();

            result.ShouldBeOfType<Failed<int>>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Success());
        }
    }
}

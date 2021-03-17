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

        [Fact]
        public void WithSuccess_MultipleSuccesses_ResultHasSuccesses()
        {
            var result = Result.WithSuccess<int>(new Success("success1"), new Success("success2"));

            result.ShouldBe(new Passed<int>(default,
                                            new Success("success1"),
                                            new Success("success2")));
        }
    }
}

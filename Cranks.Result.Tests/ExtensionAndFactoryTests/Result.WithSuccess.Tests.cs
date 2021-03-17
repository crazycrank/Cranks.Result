using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultWithSuccessTests
    {
        [Fact]
        public void WithSuccess_ResultHasSuccess()
        {
            var result = Result.WithSuccess(new Success());

            result.ShouldBeOfType<Passed>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Success());
        }

        [Fact]
        public void WithSuccessOfTSuccess_InstantiatesTSuccess_ResultHasSuccess()
        {
            var result = Result.WithSuccess<Success>();

            result.ShouldBeOfType<Passed>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Success());
        }

        [Fact]
        public void WithSuccess_OnFailed_StaysFailed()
        {
            var result = Result.Fail()
                               .WithSuccess<Success>();

            result.ShouldBeOfType<Failed>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Success());
        }
    }
}

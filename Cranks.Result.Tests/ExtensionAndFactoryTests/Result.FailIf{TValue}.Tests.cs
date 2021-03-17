using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultFailIfOfTValueTests
    {
        [Fact]
        public void FailIf_True_ShouldBeFailed()
        {
            var result = Result.FailIf<string>(true);

            result.ShouldBeOfType<Failed<string>>();
        }

        [Fact]
        public void FailIf_False_WithoutValue_ShouldBePassed_AndThrowWhenAccessingValue()
        {
            var result = Result.FailIf<string>(false);

            result.ShouldBeOfType<Passed<string>>();
            ShouldThrowExtensions.ShouldThrow<ResultException>(() => result.Value);
        }

        [Fact]
        public void FailIf_False_WithValue_ShouldBePassed_AndSetValue()
        {
            var result = Result.FailIf(false, "value");

            result.ShouldBeOfType<Passed<string>>();
            result.Value.ShouldBe("value");
        }
    }
}

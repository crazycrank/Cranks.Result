using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultPassIfOfTValueTests
    {
        [Fact]
        public void PassIf_True_WithoutValue_ShouldBePassed_AndThrowWhenAccessingValue()
        {
            var result = Result.PassIf<string>(true);

            result.ShouldBeOfType<Passed<string>>();
            ShouldThrowExtensions.ShouldThrow<ResultException>(() => result.Value);
        }

        [Fact]
        public void PassIf_True_WithValue_ShouldBePassed_AndSetValue()
        {
            var result = Result.PassIf(true, "value");

            result.ShouldBeOfType<Passed<string>>();
            result.Value.ShouldBe("value");
        }

        [Fact]
        public void PassIf_False_WithValue_ShouldBeFailed()
        {
            var result = Result.PassIf<string>(false);

            result.ShouldBeOfType<Failed<string>>();
        }
    }
}

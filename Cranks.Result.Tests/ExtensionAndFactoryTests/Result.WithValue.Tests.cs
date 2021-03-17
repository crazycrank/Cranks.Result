using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultWithValueTests
    {
        [Fact]
        public void WithValue_OnPassed_NowHasValue()
        {
            var result = Result.WithValue(42);

            result.Value.ShouldBe(42);
        }

        [Fact]
        public void WithValue_OnPassed_ChangesTypeToPassedOfValue()
        {
            var pass = Result.Pass();
            var result = pass.WithValue(42);

            pass.ShouldBeOfType<Passed>();
            result.ShouldBeOfType<Passed<int>>();
        }

        [Fact]
        public void WithValue_OnFailed_ChangesTypeToFailedOfValue()
        {
            var pass = Result.Fail();
            var result = pass.WithValue(42);

            pass.ShouldBeOfType<Failed>();
            result.ShouldBeOfType<Failed<int>>();
        }

        [Fact]
        public void WithValue_MultipleTimes_RetainsLastValue()
        {
            var result = Result.WithValue(1)
                               .WithValue(2)
                               .WithValue(3);

            result.Value.ShouldBe(3);
        }
    }
}

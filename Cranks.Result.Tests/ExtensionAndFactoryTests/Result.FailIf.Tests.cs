using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultFailIfTests
    {
        [Fact]
        public void FailIf_True_ShouldBeFailed()
        {
            var result = Result.FailIf(true);

            result.ShouldBeOfType<Failed>();
        }

        [Fact]
        public void FailIf_False_ShouldBePassed()
        {
            var result = Result.FailIf(false);

            result.ShouldBeOfType<Passed>();
        }
    }
}

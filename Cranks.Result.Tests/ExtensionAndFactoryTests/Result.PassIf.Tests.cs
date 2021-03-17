using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultPassIfTests
    {
        [Fact]
        public void PassIf_True_ShouldBePassed()
        {
            var result = Result.PassIf(true);

            result.ShouldBeOfType<Passed>();
        }

        [Fact]
        public void PassIf_False_ShouldBeFailed()
        {
            var result = Result.PassIf(false);

            result.ShouldBeOfType<Failed>();
        }
    }
}

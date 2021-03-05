using Shouldly;

using Xunit;

namespace ResultZ.Tests
{
    public class SuccessTests
    {
        [Fact]
        public void Pass_WithoutMessage_WithoutReasons()
        {
            var result = Result.Pass();

            result.ShouldBeOfType<Passed>();
            result.Message.ShouldBeEmpty();
            result.Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void Pass_WithMessage_WithoutReasons()
        {
            var result = Result.Pass("message");

            result.ShouldBeOfType<Passed>();
            result.Message.ShouldBe("message");
            result.Reasons.ShouldBeEmpty();
            result.Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void Pass_WithoutMessage_WithReasons()
        {
            // TODO this sucks. passing a success to a Result.Pass uses the Succcess as the value...
            var result = Result.Pass(reasons: (Success)"success");

            result.ShouldBeOfType<Passed>();
            result.Message.ShouldBeEmpty();
            result.Reasons.Count.ShouldBe(1);

            result.Reasons[0].ShouldBeOfType<Success>();
            result.Reasons[0].Message.ShouldBe("success");
            result.Reasons[0].Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void Pass_WithMessage_WithReasons()
        {
            var result = Result.Pass("message", (Success)"success");

            result.ShouldBeOfType<Passed>();
            result.Message.ShouldBe("message");
            result.Reasons.Count.ShouldBe(1);

            result.Reasons[0].ShouldBeOfType<Success>();
            result.Reasons[0].Message.ShouldBe("success");
            result.Reasons[0].Reasons.ShouldBeEmpty();
        }
    }
}

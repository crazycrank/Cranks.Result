using Shouldly;

using Xunit;

namespace Cranks.Result.Tests
{
    public class PassTests
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
            var result = Result.Pass()
                               .WithMessage("message");

            result.ShouldBeOfType<Passed>();
            result.Message.ShouldBe("message");
            result.Reasons.ShouldBeEmpty();
            result.Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void Pass_WithoutMessage_WithReasons()
        {
            var result = Result.Pass()
                               .WithSuccess("success");

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
            var result = Result.Pass()
                               .WithSuccess("success")
                               .WithMessage("message");

            result.ShouldBeOfType<Passed>();
            result.Message.ShouldBe("message");
            result.Reasons.Count.ShouldBe(1);

            result.Reasons[0].ShouldBeOfType<Success>();
            result.Reasons[0].Message.ShouldBe("success");
            result.Reasons[0].Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void PassOfType_WithoutMessage_WithoutReasons()
        {
            var result = Result.Pass(5);

            result.ShouldBeOfType<Passed<int>>();
            result.Value.ShouldBe(5);
            result.Message.ShouldBeEmpty();
            result.Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void PassOfType_WithMessage_WithoutReasons()
        {
            var result = Result.Pass(5)
                               .WithMessage("message");

            result.ShouldBeOfType<Passed<int>>();
            result.Value.ShouldBe(5);
            result.Message.ShouldBe("message");
            result.Reasons.ShouldBeEmpty();
            result.Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void PassOfType_WithoutMessage_WithReasons()
        {
            var result = Result.Pass(5)
                               .WithSuccess("success");

            result.ShouldBeOfType<Passed<int>>();
            result.Value.ShouldBe(5);
            result.Message.ShouldBeEmpty();
            result.Reasons.Count.ShouldBe(1);

            result.Reasons[0].ShouldBeOfType<Success>();
            result.Reasons[0].Message.ShouldBe("success");
            result.Reasons[0].Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void PassOfType_WithMessage_WithReasons()
        {
            var result = Result.Pass(5)
                               .WithSuccess("success")
                               .WithMessage("message");

            result.ShouldBeOfType<Passed<int>>();
            result.Value.ShouldBe(5);
            result.Message.ShouldBe("message");
            result.Reasons.Count.ShouldBe(1);

            result.Reasons[0].ShouldBeOfType<Success>();
            result.Reasons[0].Message.ShouldBe("success");
            result.Reasons[0].Reasons.ShouldBeEmpty();
        }
    }
}

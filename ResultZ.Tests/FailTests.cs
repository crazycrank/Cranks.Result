using System;

using Shouldly;

using Xunit;

namespace ResultZ.Tests
{
    public class FailTests
    {
        [Fact]
        public void Fail_WithoutMessage_WithoutReasons()
        {
            var result = Result.Fail();

            result.ShouldBeOfType<Failed>();
            result.Message.ShouldBeEmpty();
            result.Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void Fail_WithMessage_WithoutReasons()
        {
            var result = Result.Fail()
                               .WithMessage("message");

            result.ShouldBeOfType<Failed>();
            result.Message.ShouldBe("message");
            result.Reasons.ShouldBeEmpty();
            result.Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void Fail_WithoutMessage_WithReasons()
        {
            var result = Result.Fail()
                               .WithError("error");

            result.ShouldBeOfType<Failed>();
            result.Message.ShouldBeEmpty();
            result.Reasons.Count.ShouldBe(1);

            result.Reasons[0].ShouldBeOfType<Error>();
            result.Reasons[0].Message.ShouldBe("error");
            result.Reasons[0].Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void Fail_WithMessage_WithReasons()
        {
            var result = Result.Fail()
                               .WithError("error")
                               .WithMessage("message");

            result.ShouldBeOfType<Failed>();
            result.Message.ShouldBe("message");
            result.Reasons.Count.ShouldBe(1);

            result.Reasons[0].ShouldBeOfType<Error>();
            result.Reasons[0].Message.ShouldBe("error");
            result.Reasons[0].Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void FailOfType_WithoutMessage_WithoutReasons()
        {
            var result = Result.Fail<int>();

            result.ShouldBeOfType<Failed<int>>();
            ShouldThrowExtensions.ShouldThrow<InvalidOperationException>(() => result.Value);
            result.Message.ShouldBeEmpty();
            result.Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void FailOfType_WithMessage_WithoutReasons()
        {
            var result = Result.Fail<int>()
                               .WithMessage("message");

            result.ShouldBeOfType<Failed<int>>();
            ShouldThrowExtensions.ShouldThrow<InvalidOperationException>(() => result.Value);
            result.Message.ShouldBe("message");
            result.Reasons.ShouldBeEmpty();
            result.Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void FailOfType_WithoutMessage_WithReasons()
        {
            var result = Result.Fail<int>()
                               .WithError("error");

            result.ShouldBeOfType<Failed<int>>();
            ShouldThrowExtensions.ShouldThrow<InvalidOperationException>(() => result.Value);
            result.Message.ShouldBeEmpty();
            result.Reasons.Count.ShouldBe(1);

            result.Reasons[0].ShouldBeOfType<Error>();
            result.Reasons[0].Message.ShouldBe("error");
            result.Reasons[0].Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void FailOfType_WithMessage_WithReasons()
        {
            var result = Result.Fail<int>()
                               .WithError("error")
                               .WithMessage("message");

            result.ShouldBeOfType<Failed<int>>();
            ShouldThrowExtensions.ShouldThrow<InvalidOperationException>(() => result.Value);
            result.Message.ShouldBe("message");
            result.Reasons.Count.ShouldBe(1);

            result.Reasons[0].ShouldBeOfType<Error>();
            result.Reasons[0].Message.ShouldBe("error");
            result.Reasons[0].Reasons.ShouldBeEmpty();
        }
    }
}

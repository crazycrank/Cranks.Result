using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultFailOfTValueTests
    {
        [Fact]
        public void Failed_FieldsShouldBeCorrect()
        {
            var result = Result.Fail<int>();

            result.HasFailed.ShouldBeTrue();
            result.HasPassed.ShouldBeFalse();
            ShouldThrowExtensions.ShouldThrow<ResultException>(() => result.Value);
        }

        [Fact]
        public void Fail_CreatesEmptyFailed()
        {
            var result = Result.Fail<int>();

            result.ShouldBeOfType<Failed<int>>();
            result.Message.ShouldBeEmpty();
            result.Causes.ShouldBeEmpty();
        }

        [Fact]
        public void Fail_FromPassed_BecomesFailed_DropsValue()
        {
            var result = Result.Pass(42)
                               .Fail();

            result.ShouldBeOfType<Failed<int>>();
            ShouldThrowExtensions.ShouldThrow<ResultException>(() => result.Value);
        }

        [Fact]
        public void Fail_WithData_KeepsData()
        {
            var result = Result.WithMessage<int>("message")
                               .WithSuccess("success")
                               .Fail();

            result.ShouldBeOfType<Failed<int>>();
            result.Message.ShouldBe("message");
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Success("success"));
        }

        [Fact]
        public void Fail_OfFailed_CreatesNewInstance()
        {
            var result1 = Result.Fail<int>();
            var result2 = result1.Fail();

            result2.ShouldNotBeSameAs(result1);
        }
    }
}

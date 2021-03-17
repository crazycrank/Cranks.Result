using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultPassTests
    {
        [Fact]
        public void Pass_FieldsShouldBeCorrect()
        {
            var result = Result.Pass();

            result.HasFailed.ShouldBeFalse();
            result.HasPassed.ShouldBeTrue();
        }

        [Fact]
        public void Pass_CreatesEmptyPassed()
        {
            var result = Result.Pass();

            result.ShouldBeOfType<Passed>();
            result.Message.ShouldBeEmpty();
            result.Causes.ShouldBeEmpty();
        }

        [Fact]
        public void Pass_FromFailed_StayFailed()
        {
            var result = Result.Fail()
                               .Pass();

            result.ShouldBeOfType<Failed>();
        }

        [Fact]
        public void Pass_WithData_KeepsData()
        {
            var result = Result.WithMessage("message")
                               .WithSuccess("success")
                               .Pass();

            result.ShouldBeOfType<Passed>();
            result.Message.ShouldBe("message");
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Success("success"));
        }

        [Fact]
        public void Pass_OfPassed_CreatesNewInstance()
        {
            var result1 = Result.Pass();
            var result2 = result1.Pass();

            result2.ShouldNotBeSameAs(result1);
        }
    }

    public record MyError(int Value) : Error;
}

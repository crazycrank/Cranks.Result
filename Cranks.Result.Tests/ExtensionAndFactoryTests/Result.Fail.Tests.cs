using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultFailTests
    {
        [Fact]
        public void Failed_FieldsShouldBeCorrect()
        {
            var result = Result.Fail();

            result.HasFailed.ShouldBeTrue();
            result.HasPassed.ShouldBeFalse();
        }

        [Fact]
        public void Fail_CreatesEmptyFailed()
        {
            var result = Result.Fail();

            result.ShouldBeOfType<Failed>();
            result.Message.ShouldBeEmpty();
            result.Causes.ShouldBeEmpty();
        }

        [Fact]
        public void Fail_FromPassed_BecomesFailed()
        {
            var result = Result.Pass()
                               .Fail();

            result.ShouldBeOfType<Failed>();
        }

        [Fact]
        public void Fail_WithData_KeepsData()
        {
            var result = Result.WithMessage("message")
                               .WithSuccess("success")
                               .Fail();

            result.ShouldBeOfType<Failed>();
            result.Message.ShouldBe("message");
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Success("success"));
        }

        [Fact]
        public void Fail_OfFailed_CreatesNewInstance()
        {
            var result1 = Result.Fail();
            var result2 = result1.Fail();

            result2.ShouldNotBeSameAs(result1);
        }
    }
}

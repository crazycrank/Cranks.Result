using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultPassOfTValueTests
    {
        [Fact]
        public void Pass_FieldsShouldBeCorrect()
        {
            var result = Result.Pass<int>();

            result.HasFailed.ShouldBeFalse();
            result.HasPassed.ShouldBeTrue();
        }

        [Fact]
        public void Pass_CreatesEmptyFailed()
        {
            var result = Result.Pass(42);

            result.ShouldBeOfType<Passed<int>>();
            result.Message.ShouldBeEmpty();
            result.Causes.ShouldBeEmpty();
            result.Value.ShouldBe(42);
        }

        [Fact]
        public void Pass_WithoutValueOfStruct_ReturnsDefaultValue()
        {
            var result = Result.Pass<int>();

            result.Value.ShouldBe(default);
        }

        [Fact]
        public void Pass_WithoutValueOfObject_ThrowsWhenAccessingValue()
        {
            var result = Result.Pass<object>();

            ShouldThrowExtensions.ShouldThrow<ResultException>(() => result.Value);
        }

        [Fact]
        public void Pass_FromFailed_StayFailed()
        {
            var result = Result.Fail<int>()
                               .Pass();

            result.ShouldBeOfType<Failed<int>>();
        }

        [Fact]
        public void Pass_WithData_KeepsData()
        {
            var result = Result.WithMessage<int>("message")
                               .WithSuccess("success")
                               .Pass();

            result.ShouldBeOfType<Passed<int>>();
            result.Message.ShouldBe("message");
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Success("success"));
        }

        [Fact]
        public void Pass_OfPassed_CreatesNewInstance()
        {
            var result1 = Result.Pass<int>();
            var result2 = result1.Pass();

            result2.ShouldNotBeSameAs(result1);
        }
    }
}

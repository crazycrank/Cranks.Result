using Shouldly;

using Xunit;

namespace Cranks.Result.Tests
{
    public class GenericHandlingValidation
    {
        [Fact]
        public void CallingWithError_InAnUpcastedPassedOfType_ReturnsFailedOfType()
        {
            IResult result = Result.Pass("value");
            var result2 = result.WithError("error");

            result2.ShouldBeOfType<Failed<string>>();
            result2.Causes.ShouldContain(new Error("error"));
        }

        [Fact]
        public void CallingWithSuccess_InAnUpcastedPassedOfType_ReturnsPassedOfType()
        {
            IResult result = Result.Pass("value");
            var result2 = result.WithSuccess("success");

            result2.ShouldBeOfType<Passed<string>>();
            result2.Causes.ShouldContain(new Success("success"));
        }

        [Fact]
        public void CallingWithMessage_InAnUpcastedFailedOfType_IsStillFailedOfType()
        {
            IResult result = Result.Fail<string>();
            var result2 = result.WithMessage("message");

            result2.ShouldBeOfType<Failed<string>>();
            result2.Message.ShouldBe("message");
        }

        [Fact]
        public void CallingWithMessage_InAnUpcastedPassedOfType_IsStillPassedOfType()
        {
            IResult result = Result.Pass("value");
            var result2 = result.WithMessage("message");

            result2.ShouldBeOfType<Passed<string>>();
            result2.Message.ShouldBe("message");
        }
    }
}

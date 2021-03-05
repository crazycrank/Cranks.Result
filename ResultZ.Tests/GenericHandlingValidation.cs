using Shouldly;

using Xunit;

namespace ResultZ.Tests
{
    public class GenericHandlingValidation
    {
        [Fact]
        public void CallingWithError_InAnUpcastedResultOfType_ReturnsFailedOfType()
        {
            IResult result = Result.Pass<string>("value");
            var result2 = result.WithError("error");

            result2.ShouldBeOfType<Failed<string>>();
            result2.Reasons.ShouldContain(new Error("error"));
        }

        [Fact]
        public void CallingWithSuccess_InAnUpcastedResultOfType_ReturnsPassedOfType()
        {
            IResult result = Result.Pass<string>("value");
            var result2 = result.WithSuccess("success");

            result2.ShouldBeOfType<Passed<string>>();
            result2.Reasons.ShouldContain(new Success("success"));
        }
    }
}

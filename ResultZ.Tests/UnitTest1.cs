using ResultZ.Reasons;
using ResultZ.Results;

using Shouldly;

using Xunit;

namespace ResultZ.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var result = Result.Pass();

            result.ShouldBeOfType<Passed>();
            result.Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void Test2()
        {
            var result = Result.Pass("value");

            result.ShouldBeOfType<Passed<string>>();
            result.Reasons.ShouldBeEmpty();
            result.Value.ShouldBe("value");
        }

        [Fact]
        public void Test3()
        {
            var result = Result.Pass("value")
                               .WithError("error");

            result.ShouldBeOfType<Failed<string>>();
            result.Reasons.ShouldContain(new Error("error"));
            result.Value.ShouldBeNull();
        }

        [Fact]
        public void Test4()
        {
            IResult result = Result.Pass("value");
            var result2 = result.WithError("error");

            result2.ShouldBeOfType<Failed<string>>();
            result2.Reasons.ShouldContain(new Error("error"));
        }

        [Fact]
        public void Test5()
        {
            var innerResult = Result.Fail(new Error("message"))
                                    .WithError("message2");

            //// Result.Failed("root", innerResult);
            //// Result.Failed(new Error("root"), innerResult);
            //// Result.Failed<Error>(innerResult);
            //// Result.Failed().WithCause("root", innerResult);
            //// Result.Failed().WithCause(new Error("root"), innerResult);
            //// Result.Failed().WithCause<Error>(innerResult);

            var t = Result.Fail(innerResult);

            t.ShouldBeOfType<Failed>();
            t.Reasons.Count.ShouldBe(1);
            var rootError = t.Reasons[0];
            ////rootError.Message.ShouldBe("root");
            rootError.Reasons.Count.ShouldBe(2);
            rootError.Reasons[0].Message.ShouldBe("message");
            rootError.Reasons[0].Reasons.ShouldBeEmpty();
            rootError.Reasons[1].Message.ShouldBe("message2");
            rootError.Reasons[1].Reasons.ShouldBeEmpty();
        }
    }
}

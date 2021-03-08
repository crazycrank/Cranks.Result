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
        public void Test5()
        {
            var innerResult = Result.Fail()
                                    .WithMessage("inner")
                                    .WithError("message")
                                    .WithError("message2");

            var result = Result.Fail()
                               .WithMessage("root")
                               .WithReason(innerResult);

            result.ShouldBeOfType<Failed>();
            result.Message.ShouldBe("root");
            result.Reasons.Count.ShouldBe(1);

            var rootError = result.Reasons[0];
            rootError.Message.ShouldBe("inner");

            rootError.Reasons.Count.ShouldBe(2);
            rootError.Reasons[0].Message.ShouldBe("message");
            rootError.Reasons[0].Reasons.ShouldBeEmpty();
            rootError.Reasons[1].Message.ShouldBe("message2");
            rootError.Reasons[1].Reasons.ShouldBeEmpty();
        }
    }
}

using System.Linq;

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
            var result = Result.Successful();

            result.ShouldBeOfType<Successful>();
            result.Reasons.ShouldBeEmpty();
        }

        [Fact]
        public void Test2()
        {
            var result = Result.Successful("value");

            result.ShouldBeOfType<Successful<string>>();
            result.Reasons.ShouldBeEmpty();
            result.Value.ShouldBe("value");
        }

        [Fact]
        public void Test3()
        {
            var result = Result.Successful("value")
                               .WithError("error");

            result.ShouldBeOfType<Failure<string>>();
            result.Reasons.ShouldContain(new Error("error"));
            result.Value.ShouldBeNull();
        }

        [Fact]
        public void Test4()
        {
            IResult result = Result.Successful("value");
            var result2 = result.WithError("error");

            result2.ShouldBeOfType<Failure<string>>();
            result2.Reasons.ShouldContain(new Error("error"));
        }

        [Fact]
        public void Test5()
        {
            var innerResult = Result.Failure(new Error("message"))
                                    .WithError("message2");

            //// Result.Failure("root", innerResult);
            //// Result.Failure(new Error("root"), innerResult);
            //// Result.Failure<Error>(innerResult);
            //// Result.Failure().WithCause("root", innerResult);
            //// Result.Failure().WithCause(new Error("root"), innerResult);
            //// Result.Failure().WithCause<Error>(innerResult);

            var t = Result.Failure(new Error("root", innerResult.Reasons));

            t.ShouldBeOfType<Failure>();
            t.Reasons.Count.ShouldBe(1);
            var rootError = t.Reasons[0];
            rootError.Message.ShouldBe("root");
            rootError.Causes.Count.ShouldBe(2);
            rootError.Causes[0].Message.ShouldBe("message");
            rootError.Causes[0].Causes.ShouldBeEmpty();
            rootError.Causes[1].Message.ShouldBe("message2");
            rootError.Causes[1].Causes.ShouldBeEmpty();
        }
    }
}

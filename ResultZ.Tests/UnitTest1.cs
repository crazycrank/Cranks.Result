using System.Reflection.Metadata.Ecma335;
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
    }
}

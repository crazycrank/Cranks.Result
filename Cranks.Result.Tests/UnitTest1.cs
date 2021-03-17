using System;

using Shouldly;

using Xunit;

namespace Cranks.Result.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var result = Result.Pass();

            result.ShouldBeOfType<Passed>();
            result.Causes.ShouldBeEmpty();
        }

        [Fact]
        public void Test2()
        {
            var result = Result.Pass("value");

            result.ShouldBeOfType<Passed<string>>();
            result.Causes.ShouldBeEmpty();
            result.Value.ShouldBe("value");
        }

        [Fact]
        public void Test3()
        {
            var result = Result.Pass("value")
                               .WithError("error");

            result.ShouldBeOfType<Failed<string>>();
            result.Causes.ShouldContain(new Error("error"));
            ShouldThrowExtensions.ShouldThrow<InvalidOperationException>(() => result.Value);
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
                               .WithCause(innerResult);

            result.ShouldBeOfType<Failed>();
            result.Message.ShouldBe("root");
            result.Causes.Count.ShouldBe(1);

            var rootError = result.Causes[0];
            rootError.Message.ShouldBe("inner");

            rootError.Causes.Count.ShouldBe(2);
            rootError.Causes[0].Message.ShouldBe("message");
            rootError.Causes[0].Causes.ShouldBeEmpty();
            rootError.Causes[1].Message.ShouldBe("message2");
            rootError.Causes[1].Causes.ShouldBeEmpty();
        }
    }
}

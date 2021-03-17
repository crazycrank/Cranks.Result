using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultWithErrorOfTValueTests
    {
        [Fact]
        public void WithError_ResultHasError()
        {
            var result = Result.WithError<int>(new Error());

            result.ShouldBeOfType<Failed<int>>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Error());
        }

        [Fact]
        public void WithErrorOfTError_InstantiatesTError_ResultHasError()
        {
            var result = Result.WithError<int, Error>();

            result.ShouldBeOfType<Failed<int>>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Error());
        }

        [Fact]
        public void WithError_OnPassed_IsNowFailed()
        {
            var result = Result.Pass<int>()
                               .WithError<Error>();

            result.ShouldBeOfType<Failed<int>>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Error());
        }

        [Fact]
        public void WithError_MultipleErrors_ResultHasErrors()
        {
            var result = Result.WithError<int>(new Error("error1"), new Error("error2"));

            result.ShouldBe(new Failed<int>(new Error("error1"),
                                            new Error("error2")));
        }
    }
}

using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultWithErrorTests
    {
        [Fact]
        public void WithError_ResultHasError()
        {
            var result = Result.WithError(new Error());

            result.ShouldBeOfType<Failed>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Error());
        }

        [Fact]
        public void WithErrorOfTError_InstantiatesTError_ResultHasError()
        {
            var result = Result.WithError<Error>();

            result.ShouldBeOfType<Failed>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Error());
        }

        [Fact]
        public void WithError_OnPassed_IsNowFailed()
        {
            var result = Result.Pass()
                               .WithError<Error>();

            result.ShouldBeOfType<Failed>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Error());
        }

        [Fact]
        public void WithError_MultipleErrors_ResultHasErrors()
        {
            var result = Result.WithError(new Error("error1"), new Error("error2"));

            result.ShouldBe(new Failed(new Error("error1"),
                                       new Error("error2")));
        }
    }
}

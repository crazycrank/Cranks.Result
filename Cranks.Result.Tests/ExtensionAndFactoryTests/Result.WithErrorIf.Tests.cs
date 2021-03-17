using Cranks.Result.Tests.Utils;

using Moq;

using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultWithErrorIfTests
    {
        [Fact]
        public void WithErrorIf_True_ResultHasError()
        {
            var result = Result.WithErrorIf(true, new Error());

            result.ShouldBeOfType<Failed>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Error());
        }

        [Fact]
        public void WithErrorIf_False_ResultDoesNotHaveError()
        {
            var result = Result.WithErrorIf(false, new Error());

            result.ShouldBeOfType<Passed>();
            result.Causes.ShouldBeEmpty();
        }

        [Fact]
        public void WithErrorIf_True_ResultHasError_AndSuccessIsIgnored()
        {
            var result = Result.WithErrorIf(true, new Error(), new Success());

            result.ShouldBeOfType<Failed>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Error());
        }

        [Fact]
        public void WithErrorIf_False_ResultHasSuccess_AndErrorIsIgnored()
        {
            var result = Result.WithErrorIf(false, new Error(), new Success());

            result.ShouldBeOfType<Passed>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Success());
        }

        [Fact]
        public void WithErrorIf_True_ErrorFunction_IsCalled()
        {
            var mock = IReasonCreatorMock.GetMock();

            Result.WithErrorIf(true, () => mock.Object.CreateError());

            mock.Verify(m => m.CreateError(), Times.Once);
        }

        [Fact]
        public void WithErrorIf_False_ErrorFunction_IsNotCalled()
        {
            var mock = IReasonCreatorMock.GetMock();

            Result.WithErrorIf(false, () => mock.Object.CreateError());

            mock.Verify(m => m.CreateError(), Times.Never);
        }

        [Fact]
        public void WithErrorIf_True_OnlyErrorFunction_IsCalled()
        {
            var mock = IReasonCreatorMock.GetMock();

            Result.WithErrorIf(true, () => mock.Object.CreateError(), () => mock.Object.CreateSuccess());

            mock.Verify(m => m.CreateError(), Times.Once);
            mock.Verify(m => m.CreateSuccess(), Times.Never);
        }

        [Fact]
        public void WithErrorIf_False_OnlySuccessFunction_IsCalled()
        {
            var mock = IReasonCreatorMock.GetMock();

            Result.WithErrorIf(false, () => mock.Object.CreateError(), () => mock.Object.CreateSuccess());

            mock.Verify(m => m.CreateError(), Times.Never);
            mock.Verify(m => m.CreateSuccess(), Times.Once);
        }
    }
}

using Moq;

using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultWithSuccessIfTests
    {
        [Fact]
        public void WithSuccessIf_True_ResultHasSuccess()
        {
            var result = Result.WithSuccessIf(true, new Success());

            result.ShouldBeOfType<Passed>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Success());
        }

        [Fact]
        public void WithSuccessIf_False_ResultDoesNotHaveSuccess()
        {
            var result = Result.WithSuccessIf(false, new Success());

            result.ShouldBeOfType<Passed>();
            result.Causes.ShouldBeEmpty();
        }

        [Fact]
        public void WithSuccessIf_True_ResultHasSuccess_AndErrorIsIgnored()
        {
            var result = Result.WithSuccessIf(true, new Success(), new Error());

            result.ShouldBeOfType<Passed>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Success());
        }

        [Fact]
        public void WithSuccessIf_False_ResultHasError_AndSuccessIsIgnored()
        {
            var result = Result.WithSuccessIf(false, new Success(), new Error());

            result.ShouldBeOfType<Failed>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Error());
        }

        [Fact]
        public void WithSuccessIf_True_SuccessFunction_IsCalled()
        {
            var mock = IReasonCreatorMock.GetMock();

            Result.WithSuccessIf(true, () => mock.Object.CreateSuccess());

            mock.Verify(m => m.CreateSuccess(), Times.Once);
        }

        [Fact]
        public void WithSuccessIf_False_SuccessFunction_IsNotCalled()
        {
            var mock = IReasonCreatorMock.GetMock();

            Result.WithSuccessIf(false, () => mock.Object.CreateSuccess());

            mock.Verify(m => m.CreateSuccess(), Times.Never);
        }

        [Fact]
        public void WithSuccessIf_True_OnlySuccessFunction_IsCalled()
        {
            var mock = IReasonCreatorMock.GetMock();

            Result.WithSuccessIf(true, () => mock.Object.CreateSuccess(), () => mock.Object.CreateError());

            mock.Verify(m => m.CreateError(), Times.Never);
            mock.Verify(m => m.CreateSuccess(), Times.Once);
        }

        [Fact]
        public void WithSuccessIf_False_OnlyErrorFunction_IsCalled()
        {
            var mock = IReasonCreatorMock.GetMock();

            Result.WithSuccessIf(false, () => mock.Object.CreateSuccess(), () => mock.Object.CreateError());

            mock.Verify(m => m.CreateError(), Times.Once);
            mock.Verify(m => m.CreateSuccess(), Times.Never);
        }
    }
}

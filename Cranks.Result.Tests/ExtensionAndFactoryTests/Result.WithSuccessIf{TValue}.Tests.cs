using Cranks.Result.Tests.Utils;

using Moq;

using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultWithSuccessIfOfTValueTests
    {
        [Fact]
        public void WithSuccessIf_True_ResultHasSuccess()
        {
            var result = Result.WithSuccessIf<int>(true, new Success());

            result.ShouldBeOfType<Passed<int>>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Success());
        }

        [Fact]
        public void WithSuccessIf_False_ResultDoesNotHaveSuccess()
        {
            var result = Result.WithSuccessIf<int>(false, new Success());

            result.ShouldBeOfType<Passed<int>>();
            result.Causes.ShouldBeEmpty();
        }

        [Fact]
        public void WithSuccessIf_True_ResultHasSuccess_AndErrorIsIgnored()
        {
            var result = Result.WithSuccessIf<int>(true, new Success(), new Error());

            result.ShouldBeOfType<Passed<int>>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Success());
        }

        [Fact]
        public void WithSuccessIf_False_ResultHasError_AndSuccessIsIgnored()
        {
            var result = Result.WithSuccessIf<int>(false, new Success(), new Error());

            result.ShouldBeOfType<Failed<int>>();
            result.Causes.ShouldHaveSingleItem();
            result.Causes.ShouldContain(new Error());
        }

        [Fact]
        public void WithSuccessIf_True_SuccessFunction_IsCalled()
        {
            var mock = IReasonCreatorMock.GetMock();

            Result.WithSuccessIf<int>(true, () => mock.Object.CreateSuccess());

            mock.Verify(m => m.CreateSuccess(), Times.Once);
        }

        [Fact]
        public void WithSuccessIf_False_SuccessFunction_IsNotCalled()
        {
            var mock = IReasonCreatorMock.GetMock();

            Result.WithSuccessIf<int>(false, () => mock.Object.CreateSuccess());

            mock.Verify(m => m.CreateSuccess(), Times.Never);
        }

        [Fact]
        public void WithSuccessIf_True_OnlySuccessFunction_IsCalled()
        {
            var mock = IReasonCreatorMock.GetMock();

            Result.WithSuccessIf<int>(true, () => mock.Object.CreateSuccess(), () => mock.Object.CreateError());

            mock.Verify(m => m.CreateError(), Times.Never);
            mock.Verify(m => m.CreateSuccess(), Times.Once);
        }

        [Fact]
        public void WithSuccessIf_False_OnlyErrorFunction_IsCalled()
        {
            var mock = IReasonCreatorMock.GetMock();

            Result.WithSuccessIf<int>(false, () => mock.Object.CreateSuccess(), () => mock.Object.CreateError());

            mock.Verify(m => m.CreateError(), Times.Once);
            mock.Verify(m => m.CreateSuccess(), Times.Never);
        }
    }
}

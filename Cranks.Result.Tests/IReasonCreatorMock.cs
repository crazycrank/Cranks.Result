using Moq;

namespace Cranks.Result.Tests
{
    public interface IReasonCreatorMock
    {
        public Error CreateError();
        public Success CreateSuccess();

        public static Mock<IReasonCreatorMock> GetMock()
        {
            var mock = new Mock<IReasonCreatorMock>();
            mock.Setup(m => m.CreateError()).Returns(new Error());
            mock.Setup(m => m.CreateSuccess()).Returns(new Success());
            return mock;
        }
    }
}

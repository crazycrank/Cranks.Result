using Moq;

namespace Cranks.Result.Tests.Utils
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

    public interface IValueFactoryMock<TValue>
    {
        public TValue Value();

        public TValue OrValue();

        public static Mock<IValueFactoryMock<TValue>> GetMock(TValue value, TValue orValue)
        {
            var mock = new Mock<IValueFactoryMock<TValue>>();
            mock.Setup(m => m.Value()).Returns(value);
            mock.Setup(m => m.OrValue()).Returns(orValue);
            return mock;
        }
    }
}

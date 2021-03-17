using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultWithPassedMessageTests
    {
        [Fact]
        public void WithPassedMessage_IsPassed_NowHasMessage()
        {
            var result = Result.Pass()
                               .WithMessage("messageOld")
                               .WithPassedMessage("messageNew");

            result.Message.ShouldBe("messageNew");
        }

        [Fact]
        public void WithPassedMessage_IsFailed_RetainsOldMessage()
        {
            var result = Result.Fail()
                               .WithMessage("messageOld")
                               .WithPassedMessage("messageNew");

            result.Message.ShouldBe("messageOld");
        }

        [Fact]
        public void WithPassedMessage_IsPassed_WithOrMessage_NowHasMessage()
        {
            var result = Result.Pass()
                               .WithMessage("messageOld")
                               .WithPassedMessage("messageNew", "messageOr");

            result.Message.ShouldBe("messageNew");
        }

        [Fact]
        public void WithPassedMessage_IsFailed_WithOrMessage_NowHasMessage()
        {
            var result = Result.Fail()
                               .WithMessage("messageOld")
                               .WithPassedMessage("messageNew", "messageOr");

            result.Message.ShouldBe("messageOr");
        }
    }
}

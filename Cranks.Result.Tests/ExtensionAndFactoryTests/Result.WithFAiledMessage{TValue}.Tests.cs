using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultWithFailedMessageOfTValueTests
    {
        [Fact]
        public void WithFailedMessage_IsFailed_NowHasMessage()
        {
            var result = Result.Fail<int>()
                               .WithMessage("messageOld")
                               .WithFailedMessage("messageNew");

            result.Message.ShouldBe("messageNew");
        }

        [Fact]
        public void WithFailedMessage_IsPassed_RetainsOldMessage()
        {
            var result = Result.Pass<int>()
                               .WithMessage("messageOld")
                               .WithFailedMessage("messageNew");

            result.Message.ShouldBe("messageOld");
        }

        [Fact]
        public void WithFailedMessage_IsFailed_WithOrMessage_NowHasMessage()
        {
            var result = Result.Fail<int>()
                               .WithMessage("messageOld")
                               .WithFailedMessage("messageNew", "messageOr");

            result.Message.ShouldBe("messageNew");
        }

        [Fact]
        public void WithFailedMessage_IsPassed_WithOrMessage_NowHasMessage()
        {
            var result = Result.Pass<int>()
                               .WithMessage("messageOld")
                               .WithFailedMessage("messageNew", "messageOr");

            result.Message.ShouldBe("messageOr");
        }
    }
}

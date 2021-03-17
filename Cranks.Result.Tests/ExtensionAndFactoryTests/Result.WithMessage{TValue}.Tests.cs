using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultWithMessageOfTValueTests
    {
        [Fact]
        public void WithMessage_NowHasMessage()
        {
            var result = Result.WithMessage("message");

            result.Message.ShouldBe("message");
        }

        [Fact]
        public void WithMessage_OnFailed_NowHasMessage()
        {
            var result = Result.Fail<int>()
                               .WithMessage("message");

            result.Message.ShouldBe("message");
        }

        [Fact]
        public void WithMessage_MultipleTimes_RetainsLastMessage()
        {
            var result = Result.WithMessage("message1")
                               .WithMessage("message2")
                               .WithMessage("message3");

            result.Message.ShouldBe("message3");
        }
    }
}

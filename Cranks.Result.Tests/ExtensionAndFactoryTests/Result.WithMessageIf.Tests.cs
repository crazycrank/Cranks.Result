using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultWithMessageIfTests
    {
        [Fact]
        public void WithMessageIf_True_NowHasMessage()
        {
            var result = Result.WithMessage("messageOld")
                               .WithMessageIf(true, "messageNew");

            result.Message.ShouldBe("messageNew");
        }

        [Fact]
        public void WithMessageIf_False_RetainsOldMessage()
        {
            var result = Result.WithMessage("messageOld")
                               .WithMessageIf(false, "messageNew");

            result.Message.ShouldBe("messageOld");
        }

        [Fact]
        public void WithMessageIf_True_WithOrMessage_NowHasMessage()
        {
            var result = Result.WithMessage("messageOld")
                               .WithMessageIf(true, "messageNew", "messageOr");

            result.Message.ShouldBe("messageNew");
        }

        [Fact]
        public void WithMessageIf_False_WithOrMessage_NowHasMessage()
        {
            var result = Result.WithMessage("messageOld")
                               .WithMessageIf(false, "messageNew", "messageOr");

            result.Message.ShouldBe("messageOr");
        }
    }
}

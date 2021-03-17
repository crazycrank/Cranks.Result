using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultWithMessageIfOfTValueTests
    {
        [Fact]
        public void WithMessageIf_True_NowHasMessage()
        {
            var result = Result.WithMessage<int>("messageOld")
                               .WithMessageIf(true, "messageNew");

            result.Message.ShouldBe("messageNew");
        }

        [Fact]
        public void WithMessageIf_False_RetainsOldMessage()
        {
            var result = Result.WithMessage<int>("messageOld")
                               .WithMessageIf(false, "messageNew");

            result.Message.ShouldBe("messageOld");
        }

        [Fact]
        public void WithMessageIf_True_WithOrMessage_NowHasMessage()
        {
            var result = Result.WithMessage<int>("messageOld")
                               .WithMessageIf(true, "messageNew", "messageOr");

            result.Message.ShouldBe("messageNew");
        }

        [Fact]
        public void WithMessageIf_False_WithOrMessage_NowHasMessage()
        {
            var result = Result.WithMessage<int>("messageOld")
                               .WithMessageIf(false, "messageNew", "messageOr");

            result.Message.ShouldBe("messageOr");
        }
    }
}

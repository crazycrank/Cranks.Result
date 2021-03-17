using Cranks.Result.Tests.Utils;

using Moq;

using Shouldly;

using Xunit;

namespace Cranks.Result.Tests.ExtensionAndFactoryTests
{
    public class ResultWithValueIfTests
    {
        [Fact]
        public void WithValueIf_True_OnPassed_NowHasValue()
        {
            var result = Result.WithValue("oldValue")
                               .WithValueIf(true, "value");

            result.ShouldBeOfType<Passed<string>>();
            result.Value.ShouldBe("value");
        }

        [Fact]
        public void WithValueIf_True_OnFailed_ValueIsDropped()
        {
            var result = Result.WithValue("oldValue")
                               .Fail()
                               .WithValueIf(true, "value");

            result.ShouldBeOfType<Failed<string>>();
            ShouldThrowExtensions.ShouldThrow<ResultException>(() => result.Value);
        }

        [Fact]
        public void WithValueIf_False_OnPassed_RetainsOldValue()
        {
            var result = Result.WithValue("oldValue")
                               .WithValueIf(false, "value");

            result.ShouldBeOfType<Passed<string>>();
            result.Value.ShouldBe("oldValue");
        }

        [Fact]
        public void WithValueIf_True_OnPassed_WithOrValue_NowHasValue()
        {
            var result = Result.WithValue("oldValue")
                               .WithValueIf(true, "value", "orValue");

            result.ShouldBeOfType<Passed<string>>();
            result.Value.ShouldBe("value");
        }

        [Fact]
        public void WithValueIf_False_OnPassed_WithOrValue_NowHasOrValue()
        {
            var result = Result.WithValue("oldValue")
                               .WithValueIf(false, "value", "orValue");

            result.ShouldBeOfType<Passed<string>>();
            result.Value.ShouldBe("orValue");
        }

        [Fact]
        public void WithValueIf_True_OnFailed_WithOrValue_ValueIsDropped()
        {
            var result = Result.WithValue("oldValue")
                               .Fail()
                               .WithValueIf(true, "value", "orValue");

            result.ShouldBeOfType<Failed<string>>();
            ShouldThrowExtensions.ShouldThrow<ResultException>(() => result.Value);
        }

        [Fact]
        public void WithValueIf_False_OnFailed_WithOrValue_ValueIsDropped()
        {
            var result = Result.WithValue("oldValue")
                               .Fail()
                               .WithValueIf(false, "value", "orValue");

            result.ShouldBeOfType<Failed<string>>();
            ShouldThrowExtensions.ShouldThrow<ResultException>(() => result.Value);
        }

        [Fact]
        public void WithValueIf_True_OnPassed_NowHasValue_WithValueFactory()
        {
            var mock = IValueFactoryMock<string>.GetMock("value", "orValue");

            var result = Result.WithValue("oldValue")
                               .WithValueIf(true, () => mock.Object.Value());

            result.ShouldBeOfType<Passed<string>>();
            result.Value.ShouldBe("value");
            mock.Verify(m => m.Value(), Times.Once);
        }

        [Fact]
        public void WithValueIf_True_OnFailed_ValueIsDropped_WithValueFactory()
        {
            var mock = IValueFactoryMock<string>.GetMock("value", "orValue");

            var result = Result.WithValue("oldValue")
                               .Fail()
                               .WithValueIf(true, () => mock.Object.Value());

            result.ShouldBeOfType<Failed<string>>();
            ShouldThrowExtensions.ShouldThrow<ResultException>(() => result.Value);
            mock.Verify(m => m.Value(), Times.Never);
        }

        [Fact]
        public void WithValueIf_False_OnPassed_RetainsOldValue_WithValueFactory()
        {
            var mock = IValueFactoryMock<string>.GetMock("value", "orValue");

            var result = Result.WithValue("oldValue")
                               .WithValueIf(false, () => mock.Object.Value());

            result.ShouldBeOfType<Passed<string>>();
            result.Value.ShouldBe("oldValue");
            mock.Verify(m => m.Value(), Times.Never);
        }

        [Fact]
        public void WithValueIf_True_OnPassed_WithOrValue_NowHasValue_WithValueFactory()
        {
            var mock = IValueFactoryMock<string>.GetMock("value", "orValue");

            var result = Result.WithValue("oldValue")
                               .WithValueIf(true, () => mock.Object.Value(), () => mock.Object.OrValue());

            result.ShouldBeOfType<Passed<string>>();
            result.Value.ShouldBe("value");
            mock.Verify(m => m.Value(), Times.Once);
            mock.Verify(m => m.OrValue(), Times.Never);
        }

        [Fact]
        public void WithValueIf_False_OnPassed_WithOrValue_NowHasOrValue_WithValueFactory()
        {
            var mock = IValueFactoryMock<string>.GetMock("value", "orValue");

            var result = Result.WithValue("oldValue")
                               .WithValueIf(false, () => mock.Object.Value(), () => mock.Object.OrValue());

            result.ShouldBeOfType<Passed<string>>();
            result.Value.ShouldBe("orValue");
            mock.Verify(m => m.Value(), Times.Never);
            mock.Verify(m => m.OrValue(), Times.Once);
        }

        [Fact]
        public void WithValueIf_True_OnFailed_WithOrValue_ValueIsDropped_WithValueFactory()
        {
            var mock = IValueFactoryMock<string>.GetMock("value", "orValue");

            var result = Result.WithValue("oldValue")
                               .Fail()
                               .WithValueIf(true, () => mock.Object.Value(), () => mock.Object.OrValue());

            result.ShouldBeOfType<Failed<string>>();
            ShouldThrowExtensions.ShouldThrow<ResultException>(() => result.Value);
            mock.Verify(m => m.Value(), Times.Never);
            mock.Verify(m => m.OrValue(), Times.Never);
        }

        [Fact]
        public void WithValueIf_False_OnFailed_WithOrValue_ValueIsDropped_WithValueFactory()
        {
            var mock = IValueFactoryMock<string>.GetMock("value", "orValue");

            var result = Result.WithValue("oldValue")
                               .Fail()
                               .WithValueIf(false, () => mock.Object.Value(), () => mock.Object.OrValue());

            result.ShouldBeOfType<Failed<string>>();
            ShouldThrowExtensions.ShouldThrow<ResultException>(() => result.Value);
            mock.Verify(m => m.Value(), Times.Never);
            mock.Verify(m => m.OrValue(), Times.Never);
        }
    }
}

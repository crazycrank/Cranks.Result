using Shouldly;

using Xunit;

namespace Cranks.Result.Tests
{
    public class IntegrationTests
    {
        [Fact]
        public void ComplexErrorTree()
        {
            var innerResult = Result.WithMessage("inner")
                                    .WithError("message1")
                                    .WithError("message2");

            var result = Result.WithMessage("root")
                               .WithCause(innerResult);

            result.ShouldBe(new Failed("root",
                                       new Failed("inner",
                                                  new Error("message1"),
                                                  new Error("message2"))));
        }

        [Fact]
        public void ComplexErrorTree_Generic()
        {
            var innerResult = Result.WithMessage<int>("inner")
                                    .WithError("message1")
                                    .WithError("message2");

            var result = Result.WithMessage<int>("root")
                               .WithCause(innerResult);

            result.ShouldBe(new Failed<int>("root",
                                            new Failed<int>("inner",
                                                            new Error("message1"),
                                                            new Error("message2"))));
        }

        [Fact]
        public void StringifyResultTree()
        {
            var innerResult = Result.WithMessage<int>("inner")
                                    .WithError("message1")
                                    .WithError("message2")
                                    .WithCause(Result.Pass(42)
                                                     .WithSuccess(new Success("success",
                                                                              new Success("innerSuccess1"),
                                                                              new Success("innerSuccess2"))));

            var result = Result.WithMessage<int>("root")
                               .WithCause(innerResult)
                               .ToString();

            result.ShouldBe("Failed { Message = root, Causes = [ Failed { Message = inner, Causes = [ Error { Message = message1 }, Error { Message = message2 }, Passed { Message = , Causes = [ Success { Message = success, Causes = [ Success { Message = innerSuccess1 }, Success { Message = innerSuccess2 } ] } ], Value = 42 } ] } ] }");
        }
    }
}

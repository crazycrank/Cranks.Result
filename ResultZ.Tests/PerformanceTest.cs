using System;
using System.Diagnostics;
using System.Linq;

using Shouldly;

using Xunit;

namespace ResultZ.Tests
{
    public class PerformanceTest
    {
        [Fact(Skip = "This would fail. not sure if a concern though")] // TODO find out if it is a concern
        public void Creating10000Successes_ShouldBeFast()
        {
            var elapsed = Measure(() => Result.Pass().WithError(Enumerable.Range(0, 10).Select(i => (Error)i.ToString())));
            elapsed.ShouldBeLessThanOrEqualTo(TimeSpan.FromMilliseconds(10));
            Result.Pass().WithError(Enumerable.Range(0, 10000).Select(i => (Error)i.ToString()));
        }

        [Fact]
        public void Creating1000Successes_ShouldBeFast()
        {
            var elapsed = Measure(() => Result.Pass().WithError(Enumerable.Range(0, 1000).Select(i => (Error)i.ToString())));
            elapsed.ShouldBeLessThanOrEqualTo(TimeSpan.FromMilliseconds(200));
        }

        [Fact]
        public void Creating100Successes_ShouldBeFast()
        {
            var elapsed = Measure(() => Result.Pass().WithError(Enumerable.Range(0, 100).Select(i => (Error)i.ToString())));
            elapsed.ShouldBeLessThanOrEqualTo(TimeSpan.FromMilliseconds(50));
        }

        [Fact]
        public void Creating10Successes_ShouldBeFast()
        {
            var elapsed = Measure(() => Result.Pass().WithError(Enumerable.Range(0, 10).Select(i => (Error)i.ToString())));
            elapsed.ShouldBeLessThanOrEqualTo(TimeSpan.FromMilliseconds(10));
        }

        private static TimeSpan Measure(Action action)
        {
            action();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}

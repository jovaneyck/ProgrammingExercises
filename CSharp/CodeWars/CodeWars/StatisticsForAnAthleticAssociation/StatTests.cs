using System;
using NUnit.Framework;

namespace CodeWars.StatisticsForAnAthleticAssociation
{
    //http://www.codewars.com/kata/55b3425df71c1201a800009c/train/csharp
    [TestFixture]
    public class StatTests
    {
        [TestCase("01|15|59, 1|47|16, 01|17|20, 1|32|34, 2|17|17", "Range: 01|01|18 Average: 01|38|05 Median: 01|32|34")]
        [TestCase("02|15|59, 2|47|16, 02|17|20, 2|32|34, 2|17|17, 2|22|00, 2|31|41", "Range: 00|31|17 Average: 02|26|18 Median: 02|22|00")]
        [TestCase("01|15|59, 1|47|6, 01|17|20, 1|32|34, 2|3|17", "Range: 00|47|18 Average: 01|35|15 Median: 01|32|34")]
        public static void AcceptanceTest(string input, string expected)
        {
            Assert.AreEqual(expected, Stat.stat(input));
        }

        [Test]
        public void ReturnsEmptyStringWhenCalledWithEmptyString()
        {
            Assert.AreEqual(string.Empty, Stat.stat(string.Empty));
        }

        [Test]
        public void CanParseTeamTimes()
        {
            var times = "01|15|59, 3|47|16";

            var parsed = Stat.ParseTimes(times);

            Assert.AreEqual(
                new[] {new TimeSpan(1, 15, 59), new TimeSpan(3, 47, 16)}, 
                parsed);
        }

        [Test]
        public void CanCalculateRange()
        {
            Assert.AreEqual(new TimeSpan(0), Stat.RangeOf(new[] {new TimeSpan(188), new TimeSpan(188)}));
            Assert.AreEqual(new TimeSpan(0,3,20), Stat.RangeOf(new[] {new TimeSpan(0,58,10), new TimeSpan(1,1,30)}));
        }

        [Test]
        public void CanRenderADuration()
        {
            Assert.AreEqual("01|01|01", Stat.Render(new TimeSpan(1,1,1)));
            Assert.AreEqual("10|58|04", Stat.Render(new TimeSpan(10,58,4)));
        }

        [Test]
        public void CanCalculateAverage()
        {
            Assert.AreEqual(new TimeSpan(1,2,3), Stat.AverageOf(new[] {new TimeSpan(1,2,3)}));
            Assert.AreEqual(new TimeSpan(0,0,20), Stat.AverageOf(new[] {new TimeSpan(0,0,10), new TimeSpan(0,0,30) }));
            Assert.AreEqual(new TimeSpan(2,2,20), Stat.AverageOf(new[] {new TimeSpan(1,1,10), new TimeSpan(3,3,30) }));
            Assert.AreEqual(new TimeSpan(0,0,1), Stat.AverageOf(new[] {new TimeSpan(0,0,1), new TimeSpan(0,0,2) }));
        }

        [Test]
        public void CanCalculateMedian()
        {
            Assert.AreEqual(new TimeSpan(1,2,3), Stat.MedianOf(new[] {new TimeSpan(1,2,3)}));
            Assert.AreEqual(new TimeSpan(2,3,4), Stat.MedianOf(new[] {new TimeSpan(1,2,3), new TimeSpan(3,5,6), new TimeSpan(2,3,4) }));
            Assert.AreEqual(
                new TimeSpan(3,4,5), 
                Stat.MedianOf(new[] {new TimeSpan(1,2,3), new TimeSpan(3,4,5), new TimeSpan(2,3,4), new TimeSpan(4,5,6), new TimeSpan(5,6,7) }));
        }

        [Test]
        public void MedianOfEvenNumberOfDurationsIsAverageOfMiddleTwo()
        {
            Assert.AreEqual(
                new TimeSpan(3,3,3), 
                Stat.MedianOf(new [] {new TimeSpan(1,1,1), new TimeSpan(2,2,2), new TimeSpan(4,4,4), new TimeSpan(5,5,5) }) );
        }

        [Test]
        public void MedianIsNotDependentOnOrderOfDurations()
        {
            Assert.AreEqual(
                new TimeSpan(2,2,2),
                Stat.MedianOf(new[] { new TimeSpan(5, 5, 5), new TimeSpan(1, 1, 1), new TimeSpan(2, 2, 2) }));

            Assert.AreEqual(
                new TimeSpan(3, 3, 3),
                Stat.MedianOf(new[] { new TimeSpan(5, 5, 5), new TimeSpan(1, 1, 1), new TimeSpan(4, 4, 4), new TimeSpan(2, 2, 2)}));
        }

        [Test]
        public void AverageRoundsCorrectly()
        {
            Assert.AreEqual(
                new TimeSpan(0,0,1), 
                Stat.AverageOf(new [] {new TimeSpan(0,0,1), new TimeSpan(0,0,2) }));

            Assert.AreEqual(
                new TimeSpan(0,1,30), 
                Stat.AverageOf(new [] {new TimeSpan(0,1,0), new TimeSpan(0,2,0) }));
        }
    }
}
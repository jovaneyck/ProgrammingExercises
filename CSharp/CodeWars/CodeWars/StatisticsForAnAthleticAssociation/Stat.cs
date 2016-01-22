using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars.StatisticsForAnAthleticAssociation
{
    public class Stat
    {
        public static string stat(string teamTimes)
        {
            if (string.Empty == teamTimes)
            {
                return string.Empty;
            }

            return CalculateStatistics(teamTimes);
        }

        private static string CalculateStatistics(string teamTimes)
        {
            var durations = ParseTimes(teamTimes);
            var range = RangeOf(durations);
            var average = AverageOf(durations);
            var median = MedianOf(durations);

            return $"Range: {Render(range)} Average: {Render(average)} Median: {Render(median)}";
        }


        public static ICollection<TimeSpan> ParseTimes(string times)
        {
            return times
                .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(stringDate => stringDate.Split('|').Select(int.Parse).ToList())
                .Select(numbers => new TimeSpan(numbers[0], numbers[1], numbers[2]))
                .ToList();
        }

        public static TimeSpan RangeOf(ICollection<TimeSpan> durations)
        {
            return durations.Max() - durations.Min();
        }

        public static string Render(TimeSpan duration)
        {
            var hours = WithLeadingZeroes(duration.Hours);
            var minutes = WithLeadingZeroes(duration.Minutes);
            var seconds = WithLeadingZeroes(duration.Seconds);

            return $"{hours}|{minutes}|{seconds}";
        }

        private static string WithLeadingZeroes(int number)
        {
            return number.ToString().PadLeft(2, '0');
        }

        public static TimeSpan AverageOf(ICollection<TimeSpan> durations)
        {
            var averageInTotalSeconds = (int) durations.Select(d=>d.TotalSeconds).Average();
            return new TimeSpan(0,0,averageInTotalSeconds);
        }

        public static TimeSpan MedianOf(ICollection<TimeSpan> timeSpans)
        {
            var evenNumberOfDurations = timeSpans.Count % 2 == 0;
            return evenNumberOfDurations 
                ? MedianOfListOfEvenLength(timeSpans) 
                : MedianOfListOfOddLength(timeSpans);
        }

        private static TimeSpan MedianOfListOfEvenLength(ICollection<TimeSpan> durations)
        {
            var durationsToSkip = durations.Count/2 - 1;
            return AverageOf(Ordered(durations).Skip(durationsToSkip).Take(2).ToList());
        }

        private static IOrderedEnumerable<TimeSpan> Ordered(ICollection<TimeSpan> durations)
        {
            return durations.OrderBy(s=>s.Ticks);
        }

        private static TimeSpan MedianOfListOfOddLength(ICollection<TimeSpan> durations)
        {
            var indexOfMiddleElement = durations.Count/2;
            return Ordered(durations).ElementAt(indexOfMiddleElement);
        }
    }
}
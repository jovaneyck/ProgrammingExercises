using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using static System.String;

namespace CodeWars.StringsMix
{
    [TestFixture]
    public static class MixingTests
    {
        /*
            We can resume the differences between s1 and s2 in the following string: "1:aaaa/2:bbb" where 1 in 1:aaaa stands for string s1 and aaaa because the maximum for a is 4. In the same manner 2:bbb stands for string s2 and bbb because the maximum for b is 3.
            The task is to produce a string in which each lowercase letters of s1 or s2 appears as many times as its maximum if this maximum is strictly greater than 1; these letters will be prefixed by the number of the string where they appear with their maximum value and :. If the maximum is in s1 as well as in s2 the prefix is =:.
            In the result, substrings will be in decreasing order of their length and when they have the same length sorted alphabetically (more precisely sorted by codepoint); the different groups will be separated by '/'.
        */
        [TestCase("Are they here", "yes, they are here", "2:eeeee/2:yy/=:hh/=:rr")]
        [TestCase("looping is fun but dangerous", "less dangerous than coding", "1:ooo/1:uuu/2:sss/=:nnn/1:ii/2:aa/2:dd/2:ee/=:gg")]
        [TestCase(" In many languages", " there's a pair of functions", "1:aaa/1:nnn/1:gg/2:ee/2:ff/2:ii/2:oo/2:rr/2:ss/2:tt")]
        [TestCase("Lords of the Fallen", "gamekult", "1:ee/1:ll/1:oo")]
        [TestCase("codewars", "codewars", "")]
        [TestCase("A generation must confront the looming ", "codewarrs", "1:nnnnn/1:ooooo/1:tttt/1:eee/1:gg/1:ii/1:mm/=:rr")]
        public static void AcceptanceTests(string first, string second, string expected)
        {
            Assert.AreEqual(expected, Mixing.Mix(first, second));
        }
    }

    public class Mixing
    {
        public static string Mix(string first, string second)
        {
            
            var firstOccurences = Occurences(first).Select(occ => new OccurenceFrom {Source= "1", Occurence = occ});
            var secondOccurences = Occurences(second).Select(occ => new OccurenceFrom { Source = "2", Occurence = occ });

            var comparisons = firstOccurences
                .Union(secondOccurences)
                .GroupBy(occ => occ.Occurence.Letter)
                .Select(grouping => BuildComparison(grouping.ToList()))
                .Select(o => o.Render())
                .OrderByDescending(rendering => rendering.Length)
                .ThenBy(id=>id, new EqualsComesLastComparer());
            return Join("/", comparisons);
        }

        private static OccurenceFrom BuildComparison(List<OccurenceFrom> occurences)
        {
            if (occurences.Count == 1)
            {
                return occurences.Single();
            }

            var bestOccurence = occurences.OrderByDescending(o=>o.Occurence.NumberOfOccurences).First();

            if (occurences.All(o=>o.Occurence.NumberOfOccurences == bestOccurence.Occurence.NumberOfOccurences))
            {
                return new OccurenceFrom {Source = "=", Occurence = bestOccurence.Occurence};
            }

            return bestOccurence;

        }
        class Occurence
        {
            public int NumberOfOccurences { get; set; }
            public char Letter { get; set; }

            public override string ToString()
            {
                return $"{Letter} occurs {NumberOfOccurences} times";
            }
        }

        class OccurenceFrom
        {
            public Occurence Occurence { get; set; }
            public string Source { get; set; }

            public override string ToString()
            {
                return $"In {Source}: {Occurence}";
            }

            public string Render()
            {
                var letters = new string(Occurence.Letter, Occurence.NumberOfOccurences);
                return $"{Source}:{letters}";
            }
        }

        private static IList<Occurence> Occurences(string text)
        {
            var lowercaseLetters = text
                .Where(c => Regex.Match($"{c}", "[a-z]").Success);

            return lowercaseLetters
                .GroupBy(letter => letter)
                .Select(grouping => new Occurence {Letter = grouping.Key, NumberOfOccurences = grouping.Count()})
                .Where(occurence => occurence.NumberOfOccurences > 1)
                .ToList();
        }
    }

    public class EqualsComesLastComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x.StartsWith("=") && y.StartsWith("="))
                return x[2].CompareTo(y[2]);
            if (x.StartsWith("="))
                return 1;
            if(y.StartsWith("="))
                return -1;

            return string.Compare(x, y, StringComparison.Ordinal);
        }
    }
}
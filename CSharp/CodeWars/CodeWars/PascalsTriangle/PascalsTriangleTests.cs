using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars.PascalsTriangle
{
    [TestFixture]
    public class PascalsTriangleTests
    {
        [TestCase(1, new [] {1})]
        [TestCase(4, new [] { 1, 1, 1, 1, 2, 1, 1, 3, 3, 1 })]
        public static void AcceptanceTest(int numberOfRows, IEnumerable<int> expectedOutput)
        {
            CollectionAssert.AreEqual(
              expectedOutput,
              Kata.PascalsTriangle(numberOfRows));
        }
    }

    public class Kata
    {
        public static IEnumerable PascalsTriangle(int numberOfRows)
        {
            return 
                Enumerable.Range(1, numberOfRows)
                    .Select(PascalRow)
                    .SelectMany(row => row.Elements);
        }

        private static PascalRow PascalRow(int row)
        {
            if (row == 1)
            {
                return new PascalRow(1);
            }

            var previousRow = PascalRow(row - 1);
            var currentRow = 
                Enumerable.Range(0, row)
                    .Select(index => previousRow.ValueAt(index - 1) + previousRow.ValueAt(index))
                    .ToList();

            return new PascalRow(currentRow);
        }
    }

    public class PascalRow
    {
        private readonly List<int> _numbers = new List<int>();

        public PascalRow(params int[] numbers)
        {
            _numbers.AddRange(numbers);
        }

        public PascalRow(IEnumerable<int> currentRow)
        {
            _numbers.AddRange(currentRow);
        }

        public IList<int> Elements => _numbers.ToList();

        public int ValueAt(int index)
        {
            if (index < 0)
                return 0;
            if (index >= _numbers.Count)
                return 0;
            return _numbers[index];
        }
    }
}
﻿using System.Linq;
using static System.String;

namespace _99BottlesOfOOP
{
    public class Bottles
    {
        public string Verse(int number)
        {
            switch (number)
            {
                case 0:
                    return "No more bottles of beer on the wall, no more bottles of beer.\r\nGo to the store and buy some more, 99 bottles of beer on the wall.";
                case 1:
                    return "1 bottle of beer on the wall, 1 bottle of beer.\r\nTake it down and pass it around, no more bottles of beer on the wall.";
                case 2:
                    return "2 bottles of beer on the wall, 2 bottles of beer.\r\nTake one down and pass it around, 1 bottle of beer on the wall.";
            }

            return $"{number} bottles of beer on the wall, {number} bottles of beer.\r\nTake one down and pass it around, {number - 1} bottles of beer on the wall.";
        }

        public string Verses(int first, int last)
        {
            var verses = 
                Enumerable
                    .Range(last, first - last + 1)
                    .Reverse()
                    .Select(Verse);

            return Join(System.Environment.NewLine+System.Environment.NewLine, verses);
        }

        public string Song()
        {
            return Verses(99, 0);
        }
    }
}
using System.Linq;
using static System.Char;

namespace CodeWars.CountConsonants
{
    public class Kata
    {
        private static readonly string Vowels = "bcdfghjklmnpqrstvwxyz";

        public static int ConsonantCount(string str)
        { 
            return str
                .ToCharArray()
                .Select(ToLower)
                .Count(c => Vowels.Contains(c));
        }
    }
}
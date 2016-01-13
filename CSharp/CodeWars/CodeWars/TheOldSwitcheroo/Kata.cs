using System.Linq;
using static System.Char;

namespace CodeWars.TheOldSwitcheroo
{
    public class Kata
    {
        public static string Encode(string str)
        {
            return
                string.Join(
                    "",
                    str
                        .ToCharArray()
                        .SelectMany(Translate));

        }

        private static string Translate(char c)
        {
            if (!IsLetter(c))
            {
                return c.ToString();
            }

            const int charOffset = 96;
            return (ToLower(c) - charOffset).ToString();
        }
    }
}
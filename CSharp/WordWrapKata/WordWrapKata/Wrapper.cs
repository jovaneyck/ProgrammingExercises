namespace WordWrapKata
{
    class Wrapper
    {
        public static string wrap(string stringToWrap, int columnLength)
        {
            if (stringToWrap.Length <= columnLength)
            {
                return stringToWrap;
            }
            else
            {
                var firstPart = stringToWrap.Substring(0, columnLength);
                string secondPart = stringToWrap.Substring(columnLength, stringToWrap.Length - columnLength);
                return firstPart + "\n" + wrap(secondPart, columnLength);
            }
        }
    }
}
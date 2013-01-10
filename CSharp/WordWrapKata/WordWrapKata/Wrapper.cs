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
                return stringToWrap.Substring(0, columnLength) + "\n" +stringToWrap.Substring(columnLength, stringToWrap.Length - columnLength);
            }
        }
    }
}
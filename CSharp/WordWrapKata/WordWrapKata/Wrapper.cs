namespace WordWrapKata
{
    class Wrapper
    {
        public static string wrap(string stringToWrap, int columnLength)
        {
            if (stringToWrap.Length <= columnLength)
                return stringToWrap;

            int lastUsefullWhiteSpaceLocation = stringToWrap.LastIndexOf(' ', columnLength);
            int indexToWrapAt;

            if (lastUsefullWhiteSpaceLocation != -1 && lastUsefullWhiteSpaceLocation < columnLength)
                indexToWrapAt = lastUsefullWhiteSpaceLocation;
            else
            {
                indexToWrapAt = columnLength;
            }

            string firstPart = stringToWrap.Substring(0, indexToWrapAt);
            string secondPart = stringToWrap.Substring(indexToWrapAt, stringToWrap.Length - indexToWrapAt).Trim(); 

            return firstPart + "\n" + wrap(secondPart, columnLength);
        }
    }
}
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
                int firstWhiteSpaceLocation = stringToWrap.IndexOf(" ");
                string firstPart;
                string secondPart;

                if(firstWhiteSpaceLocation != -1 && firstWhiteSpaceLocation < columnLength)
                {
                    firstPart = stringToWrap.Substring(0, firstWhiteSpaceLocation);
                    secondPart = stringToWrap.Substring(firstWhiteSpaceLocation, stringToWrap.Length - firstWhiteSpaceLocation).Trim();
                }
                else
                {
                    firstPart = stringToWrap.Substring(0, columnLength);
                    secondPart = stringToWrap.Substring(columnLength, stringToWrap.Length - columnLength).Trim(); 
                }


                return firstPart + "\n" + wrap(secondPart, columnLength);
            }
        }
    }
}
using System;

namespace WordWrapKata
{
    class Wrapper
    {
        public int ColumnLength { get; private set; }

        public Wrapper(int columnLength)
        {
            if (columnLength < 1)
                throw new ArgumentException("Column length must be greater or equal to 1.", "columnLength");
            this.ColumnLength = columnLength;
        }

        public string Wrap(string stringToWrap)
        {
            if (!NeedsWrapping(stringToWrap))
            {
                return stringToWrap;
            }
            return WrapStringLongerThanColumnLength(stringToWrap);
        }

        private bool NeedsWrapping(string stringToWrap)
        {
            return stringToWrap.Length > ColumnLength;
        }

        private string WrapStringLongerThanColumnLength(string stringToWrap)
        {
            int indexToWrapAt = FindIndexToWrapAt(stringToWrap);

            string firstPart = ExtractFirstPart(stringToWrap, indexToWrapAt);
            string secondPart = ExtractSecondPart(stringToWrap, indexToWrapAt);

            return firstPart + "\n" + Wrap(secondPart);
        }

        private int FindIndexToWrapAt(string stringToWrap)
        {
            int lastUsefullWhiteSpaceLocation = stringToWrap.LastIndexOf(' ', ColumnLength);
            int indexToWrapAt;

            if (HasWhiteSpaceInCurrentSection(lastUsefullWhiteSpaceLocation))
            {
                indexToWrapAt = lastUsefullWhiteSpaceLocation;
            }
            else
            {
                indexToWrapAt = ColumnLength;
            }

            return indexToWrapAt;
        }

        private bool HasWhiteSpaceInCurrentSection(int lastUsefullWhiteSpaceLocation)
        {
            return lastUsefullWhiteSpaceLocation != -1 && lastUsefullWhiteSpaceLocation < ColumnLength;
        }

        private string ExtractFirstPart(string stringToWrap, int indexToWrapAt)
        {
            return stringToWrap.Substring(0, indexToWrapAt);
        }

        private string ExtractSecondPart(string stringToWrap, int indexToWrapAt)
        {
            return stringToWrap.Substring(indexToWrapAt, stringToWrap.Length - indexToWrapAt).Trim();
        }
    }
}
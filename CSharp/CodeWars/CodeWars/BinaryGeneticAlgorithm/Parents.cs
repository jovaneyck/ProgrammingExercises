using System.Collections.Generic;
using System.Linq;

namespace CodeWars.BinaryGeneticAlgorithm
{
    public class Parents
    {

        public Parents(Chromosone father, Chromosone mother)
        {
            Father = father;
            Mother = mother;
        }

        public Chromosone Father { get; }
        public Chromosone Mother { get; }

        public Parents CrossOver(CrossOverIndexPicker indexPicker)
        {
            var indexToSplitAt = indexPicker.IndexToCrossOver(Father.Length());

            var splittedFather = Father.SplitAt(indexToSplitAt);
            var splittedMother = Mother.SplitAt(indexToSplitAt);

            return new Parents(
                splittedFather.First().Append(splittedMother.ElementAt(1)),
                splittedMother.First().Append(splittedFather.ElementAt(1)));
        }
    }
}
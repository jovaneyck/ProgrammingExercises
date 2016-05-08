using System;
using System.Linq;
using System.Reactive.Linq;

namespace BowlingKata
{
    public class BowlingScorer
    {
        public int Score(params int[] pinsDownedPerThrow)
        {
            return pinsDownedPerThrow.Sum();
        }
    }
}
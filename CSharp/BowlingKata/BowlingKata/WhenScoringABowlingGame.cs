using Xunit;

namespace BowlingKata
{
    public class WhenScoringABowlingGame
    {
        private BowlingScorer _scorer;

        public WhenScoringABowlingGame()
        {
            _scorer = new BowlingScorer();
        }

        [Fact]
        public void AllGutterBallsScoresZeroPoints()
        {
            var allGutterBalls = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            Assert.Equal(
                0,
                _scorer.Score(allGutterBalls));
        }

        [Fact]
        public void AllSinglePinsScoresTwentyPoints()
        {
            var allSinglePins = new[] { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 };
            Assert.Equal(
                20,
                _scorer.Score(allSinglePins));
        }

        [Fact]
        public void ASpareResultsInTheNextThrowCountingDouble()
        {
            var spareFollowedByHit = new[]
            {
                6, 4,
                3, 0,
                0, 0,
                0, 0,
                0, 0,
                0, 0,
                0, 0,
                0, 0,
                0, 0,
                0, 0
            };

            Assert.Equal(
                16,
                _scorer.Score(spareFollowedByHit));
        }
    }
}

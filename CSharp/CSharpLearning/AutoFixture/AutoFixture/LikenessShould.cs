using Ploeh.SemanticComparison.Fluent;
using Xunit;

namespace AutoFixture
{
    /// <summary>
    /// Some seriously powerful stuff here...
    /// </summary>
    public class LikenessShould
    {
        [Fact]
        public void MakeEqualityChecksVeryReadable()
        {
            var actual = new EmailMessage
            {
                Subject = "Subject"
            };

            var identicalButNotSame = new EmailMessage
            {
                Subject = "Subject"
            };

            var likenessThatActuallyIsEqualWithoutExtraWork =
                identicalButNotSame
                    .AsSource() //using Ploeh.SemanticComparison Nuget package!
                    .OfLikeness<EmailMessage>()
                    .CreateProxy();

            Assert.False(identicalButNotSame.Equals(actual));
            Assert.True(likenessThatActuallyIsEqualWithoutExtraWork.Equals(actual)); //look ma, no work/custom equality!

            //works because the likeness has a generated custom equality comparator. Other way around does not work:
            Assert.False(actual.Equals(likenessThatActuallyIsEqualWithoutExtraWork));
            //Too bad XUnits Assert.Equal uses default comparator :(
            Assert.NotEqual(likenessThatActuallyIsEqualWithoutExtraWork, actual);
        }
    }
}
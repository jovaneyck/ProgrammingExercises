using System.Collections.Generic;
using System.Linq;

namespace CodeWars.HowManyLicks
{
    public class Kata {
        public string TotalLicks(Dictionary<string, int> environmentVariables)
        {
            var defaultNumberOfLicks = 252;
            var actualNumberOfLicks = defaultNumberOfLicks + environmentVariables.Values.Sum();
            var baseMessage = $"It took {actualNumberOfLicks} licks to get to the tootsie roll center of a tootsie pop.";

            var challenges = environmentVariables.Where(kvp => kvp.Value > 0).ToList();
            if (!challenges.Any())
            {
                return baseMessage;
            }

            var toughestChallenge = challenges.OrderByDescending(kvp => kvp.Value).First().Key;
            var toughestChallengeMessage = $"The toughest challenge was {toughestChallenge}.";
            return $"{baseMessage} {toughestChallengeMessage}";
        }
    }
}
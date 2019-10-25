using System.Linq;
using Domain;
using Domain.Contract;
using Domain.Contract.Models;
using Shouldly;
using Xunit;
using Xunit.DataAttributes;

namespace UnitTests
{
    public class CricketScoreServiceTests
    {
        private readonly ICricketScoreService _domain;
        public CricketScoreServiceTests()
        {
            _domain = new CricketScoreService();
        }

        [Theory]
        [EmbeddedResourceAsJsonDeconstructedArray("UnitTests.MatchConfigurations.json")]
        public void When_GetScoreCard_WithValidConfigurations_ScoreCardReturns(MatchConfiguration configuration)
        {
            var result = _domain.GetScoreCard(configuration);
            result.ShouldNotBeNull();
            result.ScorePerOver.Count.ShouldBeLessThanOrEqualTo(configuration.OversLimit);
            result.ScorePerOver.First().Value.First().ScoredBy.ShouldBe(configuration.Players.First().Name);
            result.ScorePerOver.Any(x => x.Value.Sum(y => y.RunScored) > 36).ShouldBeFalse();
            result.ScorePerOver.Any(x => x.Value.Any(y=>y.RunScored > 6 || y.RunScored < -1)).ShouldBeFalse();
            result.ScorePerOver.Sum(x=>x.Value.Sum(y=>y.RunScored)).ShouldBeLessThanOrEqualTo(configuration.RunsToWin + 5);
            if (result.ResultSummary.IsBattingTeamWon)
            {
                result.ResultSummary.BallsLeft.ShouldNotBeNull();
                result.ResultSummary.WicketsLeft.ShouldNotBeNull();
                result.ResultSummary.RunsLeft.ShouldBeNull();
            }
            if (!result.ResultSummary.IsBattingTeamWon)
            {
                result.ResultSummary.BallsLeft.ShouldBeNull();
                result.ResultSummary.WicketsLeft.ShouldBeNull();
                result.ResultSummary.RunsLeft.ShouldNotBeNull();
            }
        }
    }
}

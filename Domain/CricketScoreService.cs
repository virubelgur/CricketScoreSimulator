using System.Collections.Generic;
using System.Linq;
using Domain.Contract;
using Domain.Contract.Models;
using Domain.Helpers;

namespace Domain
{
    public class CricketScoreService : ICricketScoreService
    {
        private static MatchConfiguration _matchConfig;

        private static ScoreCard _scoreCard;

        private static int _runsLeft;

        private static int _wicketsLeft;

        private static int _ballsLeft;

        public CricketScoreService()
        {
            _scoreCard = new ScoreCard();
        }

        /// <summary>
        /// Generates entire score card for the given match configurations
        /// </summary>
        /// <param name="matchConfiguration"></param>
        /// <returns>Scorecard</returns>
        public ScoreCard GetScoreCard(MatchConfiguration matchConfiguration)
        {
            _matchConfig = matchConfiguration;
            _runsLeft = matchConfiguration.RunsToWin;
            _wicketsLeft = matchConfiguration.WicketsLeft;
            _ballsLeft = matchConfiguration.OversLimit * matchConfiguration.BallsPerOver;
            return GenerateScoreCard();
        }

        private static ScoreCard GenerateScoreCard()
        {
            var firstPlayer = _matchConfig.Players[0];
            var secondPlayer = _matchConfig.Players[1];
            firstPlayer.IsPlayingCurrently = true;
            secondPlayer.IsPlayingCurrently = true;

            for (var i = _matchConfig.OversLimit + 1; i-- > 1;)
            {
                if (_wicketsLeft == 0 || _runsLeft <= 0)
                {
                    break;
                }
                var overScore = StartOver(ref firstPlayer, ref secondPlayer, _matchConfig.OversLimit - i);
                _scoreCard.ScorePerOver.Add(i, overScore);
                SwapPlayers(ref firstPlayer, ref secondPlayer);
            }

            SetMatchSummary();
            return _scoreCard;
        }

        private static List<BallData> StartOver(ref Player striker, ref Player runner, int overNumber)
        {
            var overScore = new List<BallData>();
            for (var j = 1; j <= _matchConfig.BallsPerOver; j++)
            {
                striker.Balls++;
                _ballsLeft--;

                var run = RandomScoreGenerator.GetRun(_matchConfig.PlayersProbability[striker.Name]);
                overScore.Add(new BallData()
                {
                    RunScored = run,
                    ScoredBy = striker.Name
                });
                if (run == -1)
                {
                    _wicketsLeft--;
                    striker.IsOut = true;
                    striker.IsPlayingCurrently = false;
                    striker = GetTheNextPlayer();
                    if (striker == null)
                    {
                        break;
                    }
                }
                else
                {
                    striker.Score += run;
                    _runsLeft -= run;
                    if (_runsLeft <= 0)
                    {
                        break;
                    }
                    if (run % 2 != 0)
                    {
                        SwapPlayers(ref striker, ref runner);
                    }
                }
            }

            return overScore;
        }

        private static void SwapPlayers(ref Player striker, ref Player runner)
        {
            var temp = striker;
            striker = runner;
            runner = temp;
        }

        private static Player GetTheNextPlayer()
        {
            var player = _matchConfig.Players.FirstOrDefault(x => !x.IsPlayingCurrently && !x.IsOut);
            if (player != null)
            {
                player.IsPlayingCurrently = true;
            }
            return player;
        }

        private static void SetMatchSummary()
        {
            if (_runsLeft > 0)
            {
                _scoreCard.ResultSummary = new ResultSummary()
                {
                    IsBattingTeamWon = false,
                    RunsLeft = _runsLeft
                };
            }
            else
            {
                _scoreCard.ResultSummary = new ResultSummary()
                {
                    IsBattingTeamWon = true,
                    WicketsLeft = _wicketsLeft,
                    BallsLeft = _ballsLeft
                };
            }
        }
    }
}

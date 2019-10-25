using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Domain.Contract.Models;

namespace ConsoleApp
{
    public class DisplayScoreCard
    {
        private static ScoreCard _scoreCard;

        private static MatchConfiguration _matchConfig;
        public DisplayScoreCard(ScoreCard scoreCard, MatchConfiguration matchConfiguration)
        {
            _scoreCard = scoreCard;
            _matchConfig = matchConfiguration;
        }

        /// <summary>
        /// Full score card with match summary, player scores & ball by ball commentary
        /// </summary>
        public void FullScoreCard()
        {
            BallByBallScoreCard();
            MatchSummary();
            PrintPlayersScore();
        }

        /// <summary>
        /// Ball by ball commentary
        /// </summary>
        public void BallByBallScoreCard()
        {
            var runsLeft = _matchConfig.RunsToWin;
            foreach (var score in _scoreCard.ScorePerOver)
            {
                PrintOverSummary(score.Key, runsLeft);
                runsLeft -= score.Value.Sum(x=>x.RunScored);
                PrintBalls(score.Value, _matchConfig.OversLimit - score.Key);
            }
        }

        /// <summary>
        /// Match summary with player scores 
        /// </summary>
        public void MatchSummary()
        {
            Console.WriteLine("\n-------------- Match summary --------------\n");
            if (_scoreCard.ResultSummary.IsBattingTeamWon)
            {
                Console.Write(_matchConfig.BattingTeam + " won by " + _scoreCard.ResultSummary.WicketsLeft);
                Console.Write(_scoreCard.ResultSummary.WicketsLeft > 1 ? " wickets" : " wicket");
                Console.Write(" with " + _scoreCard.ResultSummary.BallsLeft);
                Console.Write(_scoreCard.ResultSummary.BallsLeft > 1 ? " balls remaining.\n" : " ball remaining.\n");

            }
            else
            {
                Console.WriteLine(_matchConfig.BowlingTeam + " won by " + _scoreCard.ResultSummary.RunsLeft + " runs");
            }
        }

        private static void PrintOverSummary(int overNumber, int runsLeft)
        {
            Console.Write("\n" + overNumber);
            Console.Write(overNumber > 1 ? " overs left " : " over left ");
            Console.Write(runsLeft);
            Console.Write(runsLeft > 1 ? " runs to win.\n" : " run to win.\n");
        }
       
        private static void PrintBalls(IList<BallData> runsScored, int overNumber)
        {
            for (var i = 0; i < runsScored.Count; i++)
            {
                Thread.Sleep(500);
                var run = runsScored[i].RunScored;
                var scoredBy = runsScored[i].ScoredBy;
                if (run == -1)
                {
                    Console.WriteLine(overNumber + "." + (i + 1) + " " + scoredBy + " is out.");
                }
                else
                {
                    Console.Write(overNumber + "." + (i + 1) + " " + scoredBy + " scores " + run);
                    Console.Write(run > 1 ? " runs.\n" : " run.\n");
                }
                
            }
        }

        private static void PrintPlayersScore()
        {
            foreach (var player in _matchConfig.Players)
            {
                Console.Write("\n" + player.Name + " - " + player.Score);
                if (!player.IsOut && player.IsPlayingCurrently)
                {
                    Console.Write("*");
                }

                Console.Write(" runs (" + player.Balls + " balls)");
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace Domain.Helpers
{
    public static class RandomScoreGenerator
    {
        /// <summary>
        /// Generates weighted random number based on given probability 
        /// </summary>
        /// <param name="playerBattingProbability"></param>
        /// <returns>Run / Number</returns>
        public static int GetRun(List<double> playerBattingProbability)
        {
            var random = new Random();
            var score = random.NextDouble();
            var cumulativeProbabilityList = new List<double>();
            for (var i = 0; i < playerBattingProbability.Count; i++)
            {
                double cumulative = 0;
                for (var j = 0; j <= i; j++)
                {
                    cumulative += playerBattingProbability[j] / 100;
                }
                cumulativeProbabilityList.Add(cumulative);
            }
            if (score < cumulativeProbabilityList[0])
            {
                return 0;
            }

            if (score >= cumulativeProbabilityList[0] && score < cumulativeProbabilityList[1])
            {
                return 1;
            }

            if (score >= cumulativeProbabilityList[1] && score < cumulativeProbabilityList[2])
            {
                return 2;
            }

            if (score >= cumulativeProbabilityList[2] && score < cumulativeProbabilityList[3])
            {
                return 3;
            }

            if (score >= cumulativeProbabilityList[3] && score < cumulativeProbabilityList[4])
            {
                return 4;
            }

            if (score >= cumulativeProbabilityList[4] && score < cumulativeProbabilityList[5])
            {
                return 5;
            }

            if (score >= cumulativeProbabilityList[5] && score < cumulativeProbabilityList[6])
            {
                return 6;
            }

            return -1;
        }
    }
}

using System.Collections.Generic;

namespace Domain.Contract.Models
{
    public sealed class ScoreCard
    {
        public ScoreCard()
        {
            ScorePerOver = new Dictionary<int, List<BallData>>();
        }

        public ResultSummary ResultSummary { get; set; }

        public Dictionary<int, List<BallData>> ScorePerOver { get; set; }

    }

}

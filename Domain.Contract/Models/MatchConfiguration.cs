using System.Collections.Generic;

namespace Domain.Contract.Models
{
    public class MatchConfiguration
    {
        public int BallsPerOver { get; set; }

        public int OversLimit { get; set; }

        public int WicketsLeft { get; set; }

        public int RunsToWin { get; set; }

        public string BowlingTeam { get; set; }

        public string BattingTeam { get; set; }

        public List<Player> Players { get; set; }

        public Dictionary<string, List<double>> PlayersProbability { get; set; }
    }
}

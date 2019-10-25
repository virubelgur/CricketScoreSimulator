namespace Domain.Contract.Models
{
    public class ResultSummary
    {
        public bool IsBattingTeamWon { get; set; }
        
        public int? BallsLeft { get; set; }

        public int? WicketsLeft { get; set; }

        public int? RunsLeft { get; set; }
    }
}

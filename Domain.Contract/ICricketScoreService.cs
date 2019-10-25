using Domain.Contract.Models;

namespace Domain.Contract
{
    public interface ICricketScoreService
    {
        ScoreCard GetScoreCard(MatchConfiguration matchConfiguration);
    }
    
}

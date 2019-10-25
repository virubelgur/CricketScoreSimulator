using Domain.Contract.Models;

namespace Domain.Contract
{
    public interface IConfigurationReader
    {
        MatchConfiguration GetMatchConfiguration(string basePath);
    }
}

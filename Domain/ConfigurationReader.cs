using System.IO;
using System.Text.Json;
using Domain.Contract;
using Domain.Contract.Models;

namespace Domain
{
    public class ConfigurationReader : IConfigurationReader
    {
        /// <summary>
        /// Reads match configuration from given json file
        /// </summary>
        /// <returns>Match configuration</returns>
        public MatchConfiguration GetMatchConfiguration(string basePath)
        {
            var jsonAllText = File.ReadAllText(basePath);
            var matchConfiguration = JsonSerializer.Deserialize<MatchConfiguration>(jsonAllText,
                new JsonSerializerOptions() {ReadCommentHandling = JsonCommentHandling.Skip});

            return matchConfiguration;
        }
    }
}
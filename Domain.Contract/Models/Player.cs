namespace Domain.Contract.Models
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int Balls { get; set; }
        public bool IsPlayingCurrently { get; set; }
        public bool IsOut { get; set; }
    }
}

namespace Euro2020App.Models
{
    public class EuroPlayer
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }

        public int EuroTeamId { get; set; }
        public EuroTeam FK_Team { get; set; }

        public int Goals { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int OwnGoals { get; set; }

        public int EuroPositionId { get; set; }
        public EuroPosition FK_Position { get; set; }
    }
}

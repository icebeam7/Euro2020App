namespace Euro2020App.Models
{
    public class EuroTeam
    {
        public int Id { get; set; }

        public int EuroCountryId { get; set; }
        public EuroCountry FK_Country { get; set; }

        public int GroupPosition { get; set; }
        public int Games { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
        public int Points { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }

        public int EuroGroupId { get; set; }
        public EuroGroup FK_Group { get; set; }

        public int EuroCoachId { get; set; }
        public EuroCoach FK_Coach { get; set; }
    }
}

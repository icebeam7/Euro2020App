using System;

namespace Euro2020App.Models
{
    public class EuroMatch
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public int EuroGroupId { get; set; }
        public EuroGroup FK_Group { get; set; }

        public int EuroHomeTeamId { get; set; }
        public EuroTeam FK_HomeTeam { get; set; }

        public int EuroAwayTeamId { get; set; }
        public EuroTeam FK_AwayTeam { get; set; }

        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }

        public int EuroRefereeId { get; set; }
        public EuroReferee FK_Referee { get; set; }

        public DateTime ScheduledDateTime { get; set; }

        public int EuroStadiumId { get; set; }
        public EuroStadium FK_Stadium { get; set; }
    }
}

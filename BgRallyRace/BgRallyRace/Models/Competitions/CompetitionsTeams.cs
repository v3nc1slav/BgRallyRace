namespace BgRallyRace.Models
{
    using BgRallyRace.Models.Enums;
    using System;

    public class CompetitionsTeams
    {
        public int CompetitionId { get; set; }

        public Competitions.Competitions Competition { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; }

        public DriveType Drive { get; set; }

        public UseOfTurboType UseOfTurboType { get; set; }

        public int PilotId { get; set; }

        public int NavigatorId { get; set; }

        public int CarId { get; set; }

        public int Points { get; set; }

        public DateTime Time { get; set; }

    }
}

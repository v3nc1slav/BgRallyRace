namespace BgRallyRace.Models
{
    using System.Collections.Generic;

    public class Team : Teams.Teams
    {
        public int? CompetitionId { get; set; }

        public ICollection<CompetitionsTeams>? CompetitionsTeams { get; set; } = new HashSet<CompetitionsTeams>();

        public int? RallyPilotId { get; set; }

        public RallyPilots RallyPilot { get; set; } 

        public int? RallyNavigatorId { get; set; }

        public RallyNavigators RallyNavigator { get; set; }
        public int? FitterId { get; set; }

        public RallyFitters? RallyFitter { get; set; }

    }
}

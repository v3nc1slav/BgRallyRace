namespace BgRallyRace.Models
{
    public class CompetitionsTeams
    {
        public int CompetitionId { get; set; }

        public Competitions Competition { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; }
    }
}

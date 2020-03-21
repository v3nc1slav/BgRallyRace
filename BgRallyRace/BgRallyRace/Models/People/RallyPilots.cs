namespace BgRallyRace.Models
{
    using System.Collections.Generic;

    public class RallyPilots :People
    {
        public int Reflexes { get; set; }

        public int? TeamId { get; set; }

        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    }
}
namespace BgRallyRace.Models
{
    using System.Collections.Generic;

    public class RallyNavigators : People
    {
        public int Communication { get; set; }

        public int? TeamId { get; set; }

        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    }
}
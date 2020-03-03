using System.Collections.Generic;

namespace BgRallyRace.Models
{
    public class RallyFitters :People
    {
        public int Skills { get; set; }

        public int Speed { get; set; }

        public int? TeamId { get; set; }

        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    }
}
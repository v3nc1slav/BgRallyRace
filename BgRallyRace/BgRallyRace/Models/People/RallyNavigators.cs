using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BgRallyRace.Models
{
    public class RallyNavigators :People
    {
        public int Communication { get; set; }

        public int? TeamId { get; set; }

        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    }
}
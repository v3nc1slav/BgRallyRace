using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BgRallyRace.Models
{
    public class RallyNavigators :People
    {
        public int PhysicalTraining { get; set; }

        public int Pounds { get; set; }

        public int Communication { get; set; }

        public int TeamId { get; set; }

        public ICollection<TeamsRallyNavigators> Teams { get; set; } = new HashSet<TeamsRallyNavigators>();
    }
}
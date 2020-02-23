using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

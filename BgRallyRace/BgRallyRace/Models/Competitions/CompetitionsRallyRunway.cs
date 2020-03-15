using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Models
{
    public class CompetitionsRallyRunway
    {
        public int CompetitionsId { get; set; }

        public Competitions Competition { get; set; }

        public int RallyRunwayId { get; set; }

        public RallyRunway RallyRunway { get; set; }
    }
}

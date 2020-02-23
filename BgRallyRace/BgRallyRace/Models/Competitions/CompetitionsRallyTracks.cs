using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Models
{
    public class CompetitionsRallyTracks
    {
        public int CompetitionsId { get; set; }

        public Competitions Competition { get; set; }

        public int RallyTracksId { get; set; }

        public RallyTracks RallyTrack { get; set; }
    }
}

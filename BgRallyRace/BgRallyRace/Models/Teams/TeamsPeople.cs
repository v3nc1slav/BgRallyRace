using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Models.Teams
{
    public class TeamsPeople
    {
        public int ITeamsId { get; set; }

        public Teams Teams { get; set; }

        public int IPeopleId { get; set; }

        public People People { get; set; }
    }
}

namespace BgRallyRace.Models.Competitions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RaceHistory
    {
        public int Id { get; set; }

        public int CompetitionsId { get; set; }

        public string CompetitionsName { get; set; }

        public string History { get; set; } 

    }
}

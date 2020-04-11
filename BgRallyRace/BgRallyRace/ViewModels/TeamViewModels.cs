namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models;
    using BgRallyRace.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class TeamViewModels : PagesViewModels
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Cars Cars { get; set; }

        public string StartRaceDate { get; set; }

        public RallyPilots RallyPilot { get; set; }

        public List<RallyPilots> RallyPilots { get; set; }

        public RallyNavigators RallyNavigator { get; set; }

        public List<RallyNavigators> RallyNavigators { get; set; }

        public int FitterId { get; set; }

        public RallyFitters RallyFitter { get; set; }

        public Team Team { get; set; }

        public RallyRunway Runway { get; set; }

        public DriveType Drive { get; set; }

        public UseOfTurboType UseOfTurbo { get; set; }
    }
}

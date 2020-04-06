namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models;
    using System;
    using System.Collections.Generic;

    public class CompetitionsViewModels
    {
        public string Name { get; set; }

        public DateTime StartRaceDate { get; set; } =  DateTime.Now;

        public int Stages { get; set; } = 1;

        public List<int> CompetitionsRallyRunwayId { get; set; }

        public List<CompetitionsRallyRunway> CompetitionsRallyRunway { get; set; } 

        public int? TeamId { get; set; }

        public List<CompetitionsTeams>? CompetitionsTeams { get; set; } 

        public List<RallyRunway> Runways { get; set; }

    }
}

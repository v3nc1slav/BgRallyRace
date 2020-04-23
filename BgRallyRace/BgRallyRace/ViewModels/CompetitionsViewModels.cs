﻿namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models;
    using BgRallyRace.Models.Competitions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CompetitionsViewModels : PagesViewModels
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartRaceDate { get; set; } =  DateTime.Now;

        [Required]
        [Range(0, 10)]
        public int Stages { get; set; } = 1;

        [Required]
        [Range(110000, 100000000)]
        public decimal PrizeFund { get; set; }

        [Required]
        public List<int> CompetitionsRallyRunwayId { get; set; }

        public List<CompetitionsRallyRunway> CompetitionsRallyRunway { get; set; } 

        public int? TeamId { get; set; }

        public List<CompetitionsTeams>? CompetitionsTeams { get; set; }

        public List<RallyRunway> Runways { get; set; }

        public RallyRunway Runway { get; set; }

        public List<Competitions> Competitions { get; set; }

        public string Text { get; set; }
    }
}

namespace BgRallyRace.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Competitions
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartRaceDate { get; set; }

        [Required]
        public int Stages { get; set; } 

        public int CompetitionsRallyRunwayId { get; set; }

        public List<CompetitionsRallyRunway> CompetitionsRallyRunway { get; set; } = new List<CompetitionsRallyRunway>();

        public int TeamId { get; set; }

        public List<CompetitionsTeams> CompetitionsTeams { get; set; } = new List<CompetitionsTeams>();

    }
}

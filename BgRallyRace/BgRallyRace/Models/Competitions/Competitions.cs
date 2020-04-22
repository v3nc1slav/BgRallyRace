namespace BgRallyRace.Models.Competitions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

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

        [Required]
        public decimal PrizeFund { get; set; }

        public int CompetitionsRallyRunwayId { get; set; }

        public List<CompetitionsRallyRunway> CompetitionsRallyRunway { get; set; } = new List<CompetitionsRallyRunway>();

        public int CompetitionsTeamsId { get; set; }

        public List<CompetitionsTeams> CompetitionsTeams { get; set; } = new List<CompetitionsTeams>();

        public bool Applicable { get; set; } = false; 

        public bool IsDeleted { get; set; } = false;

    }
}

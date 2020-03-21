namespace BgRallyRace.Models
{
    using BgRallyRace.Models.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    public class RallyRunway
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal TrackLength { get; set; }

        [Required]
        public DifficultyType Difficulty { get; set; }

        [Required]
        public string Description { get; set; }

        public int? CompetitionsRallyTracksId { get; set; }

        public ICollection< CompetitionsRallyRunway>? CompetitionsRallyTracks { get; set; }  = new HashSet<CompetitionsRallyRunway>();
    }
}

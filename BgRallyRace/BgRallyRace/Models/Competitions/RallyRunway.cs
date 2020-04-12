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

        public string BGDifficulty
        {
            get
            {
                if (Difficulty == DifficultyType.Easy)
                {
                    return "Лесна";
                }
                else if (Difficulty == DifficultyType.Average)
                {
                    return "Средна";
                }
                else
                {
                    return "Сложна";
                }
            }
        }

        public string ShortDescription
        {
            get
            {
                return this.Description.Length > 50
                ? this.Description.Substring(0, 50) + "..."
                : this.Description;
            }
            set { }
        }

        public string ImagName { get; set; }

        public int? CompetitionsRallyTracksId { get; set; }

        public List< CompetitionsRallyRunway>? CompetitionsRallyTracks { get; set; }  = new List<CompetitionsRallyRunway>();
    }
}

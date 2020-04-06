namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class RunwayViewModels
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal TrackLength { get; set; }

        public DifficultyType Difficulty { get; set; }

        public string Description { get; set; }
    }
}

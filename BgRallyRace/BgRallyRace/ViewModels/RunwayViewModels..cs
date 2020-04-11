namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models;
    using BgRallyRace.Models.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RunwayViewModels : PagesViewModels
    {
        public int Id { get; set; }

        [Required]
        public string NameRunway { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal TrackLength { get; set; }

        public DifficultyType Difficulty { get; set; }

        public string Description { get; set; }

        public string ShortDescription  {get; set;}

        public string ImagName { get; set; }

        public List<RallyRunway> Runways { get; set; }
    }
}

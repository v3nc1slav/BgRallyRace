﻿namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models.Competitions;
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

        public string BGDifficulty {
            get {
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

        public string Description { get; set; }

        public string ShortDescription  {get; set;}

        public string ImagName { get; set; }

        public List<RallyRunway> Runways { get; set; }

        public string Text { get; set; }
    }
}

﻿using BgRallyRace.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Models
{
    public class RallyTracks
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

        public int CompetitionsRallyTracksId { get; set; }

        public ICollection< CompetitionsRallyTracks> CompetitionsRallyTracks { get; set; }  = new HashSet<CompetitionsRallyTracks>();
    }
}

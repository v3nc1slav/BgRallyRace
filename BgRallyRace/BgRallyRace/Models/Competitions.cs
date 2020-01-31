using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Models
{
    public class Competitions
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartRace { get; set; }

        [Required]
        public int RallyTracksId { get; set; }

        public RallyTracks RallyTrack { get; set; }

        [Required]
        public int TeamId { get; set; }

        public ICollection<Teams> Team { get; set; } = new HashSet<Teams>();

    }
}

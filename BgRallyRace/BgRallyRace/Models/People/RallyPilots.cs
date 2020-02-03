using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BgRallyRace.Models
{
    public class RallyPilots :People
    {
        public int PhysicalTraining { get; set; }

        public int Pounds { get; set; }

        public int Reflexes { get; set; }

        public int TeamId { get; set; }

        public ICollection<TeamsRallyPilots> Teams { get; set; } = new HashSet<TeamsRallyPilots>();
    }
}
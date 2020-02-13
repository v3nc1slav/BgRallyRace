using BgRallyRace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.ViewModels
{
    public class PilotViewModels
    {
        public ICollection<RallyPilots>? Pilot { get; set; } = new HashSet<RallyPilots>();
    }
}

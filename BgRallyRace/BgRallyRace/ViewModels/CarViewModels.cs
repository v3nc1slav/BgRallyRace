using BgRallyRace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.ViewModels
{
    public class CarViewModels
    {
        public Cars Cars { get; set; }
        public Cars Car { get; set; }
        public string Name { get; set; }
        public Engines Engines { get; set; }

    }
}

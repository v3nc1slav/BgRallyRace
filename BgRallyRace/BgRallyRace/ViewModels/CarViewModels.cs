using BgRallyRace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.ViewModels
{
    public class CarViewModels
    {
        public Task< List<Cars>> Cars { get; set; }
        public Task<Engines> Engines { get; set; }

        //car.result.Engines.Speed
    }
}

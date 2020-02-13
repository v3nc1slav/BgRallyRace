using BgRallyRace.Data;
using BgRallyRace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
    public interface IRallyPilotsServices :IPeople
    {
        async Task IncreaseReflexes(int id, int variable);
        async Task DecreaseReflexes(int id, int variable);
        async Task<RallyPilots> GetPilot(int id);

    }
}

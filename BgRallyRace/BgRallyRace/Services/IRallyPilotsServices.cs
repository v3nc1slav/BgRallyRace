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
      Task IncreaseReflexes(int id, int variable);
      Task DecreaseReflexes(int id, int variable);
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        ICollection<RallyPilots>? GetPilots(string user);
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        Task<RallyPilots> GetPilot(int id);
    }
}

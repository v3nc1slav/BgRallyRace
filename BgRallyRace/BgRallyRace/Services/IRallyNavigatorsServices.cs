using BgRallyRace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
    public interface IRallyNavigatorsServices : IPeople
    {
        async Task IncreaseCommunication(int id, int variable);
        async Task DecreaseCommunication(int id, int variable);
        async Task<RallyNavigators> GetNavigators(int id);
    }
}

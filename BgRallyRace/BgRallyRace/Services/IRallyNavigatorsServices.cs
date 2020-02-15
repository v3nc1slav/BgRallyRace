using BgRallyRace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
    public interface IRallyNavigatorsServices : IPeople
    {
       Task IncreaseCommunication(int id, int variable);
       Task DecreaseCommunication(int id, int variable);
        ICollection<RallyNavigators>? GetNavigators(string user);
    }
}

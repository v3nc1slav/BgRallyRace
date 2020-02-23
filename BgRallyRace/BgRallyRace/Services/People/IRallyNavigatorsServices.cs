using BgRallyRace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
    public interface IRallyNavigatorsServices : IPeople
    {
       void IncreaseCommunication(int id, int variable);
       void DecreaseCommunication(int id, int variable);
        ICollection<RallyNavigators>? GetNavigators(string user);
    }
}

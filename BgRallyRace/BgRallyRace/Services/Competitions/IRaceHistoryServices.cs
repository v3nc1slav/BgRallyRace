using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services.Competitions
{
    public interface IRaceHistoryServices
    {
        Task CreateHistory(int id, string name, string history);

        void AddHistory(string input);
    }
}

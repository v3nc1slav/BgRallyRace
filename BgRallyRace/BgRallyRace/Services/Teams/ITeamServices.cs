using BgRallyRace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
    public interface ITeamServices
    {
        void CreateTeam(string text, string user);

        Team FindUser(string user);
    }
}

namespace BgRallyRace.Services.Runways
{
    using BgRallyRace.Models;
    using BgRallyRace.Models.Competitions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRunwaysServices
    {
        Task<RallyRunway> GetRunway();

        List<RallyRunway> GetAllRunways();

        RallyRunway GetRally(int id);

        public RallyRunway GetRunwayForCurrentRace();
    }
}

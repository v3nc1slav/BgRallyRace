namespace BgRallyRace.Services.Runways
{
    using BgRallyRace.Models;
    using BgRallyRace.Models.Competitions;
    using System.Collections.Generic;

    public interface IRunwaysServices
    {
        List<RallyRunway> GetAllRunways();

        RallyRunway GetRally(int id);

        public RallyRunway GetRunwayForCurrentRace();
    }
}

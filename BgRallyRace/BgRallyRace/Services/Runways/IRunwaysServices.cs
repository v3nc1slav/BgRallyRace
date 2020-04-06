namespace BgRallyRace.Services.Runways
{
    using BgRallyRace.Models;
    using System.Collections.Generic;

    public interface IRunwaysServices
    {
        List<RallyRunway> GetALLRunways();
    }
}

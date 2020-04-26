namespace BgRallyRace.Services.Runways
{
    using BgRallyRace.Models.Competitions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRunwaysServices
    {
        Task<RallyRunway> GetRunwayForRaceAsync();

        Task<List<RallyRunway>> GetAllRunwaysAsync();

        Task<RallyRunway> GetRunwayForCurrentRaceAsync();

        Task<RallyRunway> GetRunwayAsync(int id);

    }
}

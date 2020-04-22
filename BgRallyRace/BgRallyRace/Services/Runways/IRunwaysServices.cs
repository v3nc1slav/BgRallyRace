namespace BgRallyRace.Services.Runways
{
    using BgRallyRace.Models.Competitions;
    using BgRallyRace.ViewModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRunwaysServices
    {
        Task<RallyRunway> GetRunwayForRace();

        List<RallyRunway> GetAllRunways();

        public RallyRunway GetRunwayForCurrentRace();

        Task<RallyRunway> GetRunway(int id);

    }
}

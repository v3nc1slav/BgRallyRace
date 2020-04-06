namespace BgRallyRace.Services.Runways
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class RunwaysServices : IRunwaysServices
    {
        private readonly ApplicationDbContext dbContext;

        public RunwaysServices (ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<RallyRunway> GetALLRunways()
        {
            var runways = dbContext.RallyRunways.ToList();
            return runways;
        }
    }
}

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
        public List<RallyRunway> GetAllRunways()
        {
            var runways = dbContext.RallyRunways.ToList();
            return runways;
        }

        public RallyRunway GetRally(int id)
        {
            var runway = dbContext.RallyRunways.Where(x => x.Id == id).FirstOrDefault();
            return runway;          
        }
    }
}

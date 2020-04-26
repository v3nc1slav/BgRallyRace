namespace BgRallyRace.Services.Runways
{
    using BgRallyRace.Data;
    using BgRallyRace.Models.Competitions;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RunwaysServices : IRunwaysServices
    {
        private readonly ApplicationDbContext dbContext;

        public RunwaysServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task< List<RallyRunway>> GetAllRunwaysAsync()
        {
            var runways = dbContext.RallyRunways.Where(x => x.IsDeleted == false).ToListAsync();
            return runways.Result;
        }

        public async Task<RallyRunway> GetRunwayForCurrentRaceAsync()
        {
            var runway = await dbContext.CompetitionsRallyRunway
                .Where(x => x.Competition.Applicable == true)
                .Select(x => new RallyRunway
                {
                    Id = x.RallyRunway.Id,
                    Name = x.RallyRunway.Name,
                    Difficulty = x.RallyRunway.Difficulty,
                    TrackLength = x.RallyRunway.TrackLength,
                    ImagName = x.RallyRunway.ImagName,

                })
                .FirstAsync();
            return runway;
        }

        public async Task<RallyRunway> GetRunwayForRaceAsync()
        {
            var runway = await dbContext.CompetitionsRallyRunway
                .Where(x => x.Competition.Applicable == true && x.Competition.IsDeleted == false)
                .Select(x => new RallyRunway
                {
                    Id = x.RallyRunway.Id,
                    Name = x.RallyRunway.Name,
                    Difficulty = x.RallyRunway.Difficulty,
                    TrackLength = x.RallyRunway.TrackLength,
                })
                .FirstAsync();
            return runway;
        }

        public async Task<RallyRunway> GetRunwayAsync(int id)
        {
            var runway = await dbContext.RallyRunways
                .Where(x => x.Id == id && x.IsDeleted == false)
                .FirstAsync();
            return runway;
        }

    }
}



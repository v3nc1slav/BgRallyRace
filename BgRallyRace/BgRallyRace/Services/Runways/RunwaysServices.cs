namespace BgRallyRace.Services.Runways
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.Models.Competitions;
using BgRallyRace.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RunwaysServices : IRunwaysServices
    {
        private readonly ApplicationDbContext dbContext;

        public RunwaysServices (ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<RallyRunway> GetAllRunways()
        {
            var runways = dbContext.RallyRunways.Where(x=>x.IsDeleted == false).ToList();
            return runways;
        }

        public RallyRunway GetRunwayForCurrentRace()
        {
            var runway = dbContext.CompetitionsRallyRunway
                .Where(x => x.Competition.Applicable == true)
                .Select(x => new RallyRunway
                { 
                    Id  = x.RallyRunway.Id,
                    Name = x.RallyRunway.Name,
                    Difficulty = x.RallyRunway.Difficulty,
                    TrackLength = x.RallyRunway.TrackLength,
                    ImagName = x.RallyRunway.ImagName,
                    
                })
                .First();
            return runway;
        }

        public async Task< RallyRunway> GetRunwayForRace()
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

        public async Task<RallyRunway> GetRunway(int id)
        {
            var runway = await dbContext.RallyRunways
                .Where(x => x.Id == id && x.IsDeleted==false)
                .FirstAsync();
            return runway;
        }

        public async Task<string> EditRunways(RunwayViewModels newRunway)
        {
            var oldRunway =  GetRunway(newRunway.Id).GetAwaiter().GetResult();
            oldRunway.Name = newRunway.NameRunway;
            oldRunway.Description = newRunway.Description;
            oldRunway.Difficulty = newRunway.Difficulty;
            oldRunway.ImagName = newRunway.ImagName;
            oldRunway.TrackLength = newRunway.TrackLength;
            await dbContext.SaveChangesAsync();
            return "Пистата, е променена успесно.";
        }

        public async Task<string> DeleteRunways(int id)
        {
            var runway = GetRunway(id).GetAwaiter().GetResult();
            runway.IsDeleted = true;
            await dbContext.SaveChangesAsync();
            return "Пистата, е изтрита успесно.";
        }

    }
}

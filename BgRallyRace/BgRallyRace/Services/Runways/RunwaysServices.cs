namespace BgRallyRace.Services.Runways
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.Models.Competitions;
    using System;
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

        public RallyRunway GetRunwayForCurrentRace()
        {
            var runway = dbContext.CompetitionsRallyRunway
                .Where(x => x.Competition.StartRaceDate > DateTime.Now)
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
    }
}

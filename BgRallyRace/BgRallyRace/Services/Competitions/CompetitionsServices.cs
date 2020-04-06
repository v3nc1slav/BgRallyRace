namespace BgRallyRace.Services.Competitions
{
    using BgRallyRace.Data;
    using System;
    using System.Linq;

    public class CompetitionsServices : ICompetitionsServices
    {
        private readonly ApplicationDbContext dbContext;

        public CompetitionsServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DateTime GetStartDate()
        {
            var date = dbContext.Competitions.Where(x => x.StartRaceDate > DateTime.Now).Select(x => x.StartRaceDate).FirstOrDefault();
            return date;
        }
    }
}

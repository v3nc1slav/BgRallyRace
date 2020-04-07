namespace BgRallyRace.Services.Competitions
{
    using BgRallyRace.Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompetitionsServices : ICompetitionsServices
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICarServices cars;

        public CompetitionsServices()
        {
            
        }

        public CompetitionsServices(ApplicationDbContext dbContext, ICarServices carServices)
        {
            this.dbContext = dbContext;
            this.cars = carServices;
        }

        public DateTime GetStartDate()
        {
            var date = dbContext
                .Competitions
                .Where(x => x.StartRaceDate > DateTime.Now)
                .Select(x => x.StartRaceDate)
                .FirstOrDefault();
            return date;
        }

        public async Task HasIsStartedAsync()
        {
            var date = this.GetStartDate();
            var nowDate = DateTime.Now;
            if (date == nowDate)
            {
                var car = dbContext.Competitions.First();
                car.CompetitionsRallyRunwayId = 1;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

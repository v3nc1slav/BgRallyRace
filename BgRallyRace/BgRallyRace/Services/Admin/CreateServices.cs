namespace BgRallyRace.Services.Admin
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.Models.Competitions;
    using BgRallyRace.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class CreateServices : ICreateServices
    {
        private readonly ApplicationDbContext dbContext;

        public CreateServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public async Task<string> CreateCompetitionsAsync(CompetitionsViewModels input)
        {
            var id =  dbContext.Add(new Competitions
            {
                Name = input.Name,
                StartRaceDate = input.StartRaceDate,
                Stages = input.Stages,
                PrizeFund = input.PrizeFund,
            });
            await dbContext.SaveChangesAsync();

            for (int i = 0; i < input.CompetitionsRallyRunwayId.Count; i++)
            {
                dbContext.CompetitionsRallyRunway.Add(new CompetitionsRallyRunway
                {
                    RallyRunwayId = input.CompetitionsRallyRunwayId[i],
                    CompetitionsId = id.Entity.Id,
                });
            }
            await dbContext.SaveChangesAsync();
            return "Състезянието е успешно създадено.";
        }

        public async Task<string> CreateRunwayAsync(RunwayViewModels input)
        {
            dbContext.RallyRunways.Add(new RallyRunway { Name = input.NameRunway, TrackLength = input.TrackLength,
                Difficulty = input.Difficulty, Description = input.Description });
            await dbContext.SaveChangesAsync();
            return "Пистата е успешно създадено.";

        }

        public async Task<string> CreatePilotAsync(PilotViewModels input)
        {
            string? firstN = input.FirstName;
            string? lastN = input.LastName;


            if (firstN == null)
            {
                firstN = await GeneratingFirstName();
              
            }
            if (lastN == null)
            {
                lastN = await GeneratingLastName();
            }

            var rallyPilot = dbContext.RallyPilots.Add(new RallyPilots
            {
                FirstName = firstN,
                LastName = lastN,
                Age = input.Age,
                Salary = input.Salary,
                Concentration = input.Concentration,
                Experience = input.Experience,
                Energy = input.Energy,
                Devotion = input.Devotion,
                PhysicalTraining = input.PhysicalTraining,
                Reflexes = input.Reflexes,
                Pounds = input.Pounds,
            });
            await dbContext.SaveChangesAsync();
            return "Пилота е успешно създадено.";

        }

        public async Task<string> CreateNavigatorAsync(NavigatorViewModels input)
        {
        
            string? firstN = input.FirstName;
            string? lastN = input.LastName;

            if (firstN == null)
            {
                firstN = await GeneratingFirstName();
            }
            if (lastN == null)
            {
                lastN = await GeneratingLastName();
            }

            var rallyPilot = dbContext.RallyNavigators.Add(new RallyNavigators
            {
                FirstName = firstN,
                LastName = lastN,
                Age = input.Age,
                Salary = input.Salary,
                Concentration = input.Concentration,
                Experience = input.Experience,
                Energy = input.Energy,
                Devotion = input.Devotion,
                PhysicalTraining = input.PhysicalTraining,
                Communication = input.Communication,
                Pounds = input.Pounds,
            });
            await dbContext.SaveChangesAsync();
            return "Навигатора е успешно създадено.";

        }

        public async Task<string> CreateParts(PartsViewModels input)
        {
            dbContext.Add(new PartsCars
            {
                Type = input.Type,
                Name = input.Name,
                Price = input.Price,
                Strength = input.Strength,
                Speed = input.Speed,
            });
            await dbContext.SaveChangesAsync();
            return "Часта е успешно създадено.";

        }

        private async Task<string> GeneratingFirstName()
        {
            Random rnd = new Random();
            int first = rnd.Next(1, 100);
            var name = dbContext.FirstNames.Select(x => new { x.FirstName, x.Id })
           .FirstOrDefaultAsync(x => x.Id == first);
            return name.Result.FirstName;
        }

        private async Task<string> GeneratingLastName()
        {
            Random rnd = new Random();
            int last = rnd.Next(1, 100);
            var name = dbContext.LastNames.Select(x => new { x.LastName, x.Id })
            .FirstOrDefaultAsync(x => x.Id == last);
            return name.Result.LastName;
        }
    }
}

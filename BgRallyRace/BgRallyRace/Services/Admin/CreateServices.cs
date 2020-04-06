namespace BgRallyRace.Services.Admin
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.ViewModels;
    using System;
    using System.Linq;

    public class CreateServices : ICreateServices
    {
        private readonly ApplicationDbContext dbContext;

        public CreateServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public void CreateCompetitions(CompetitionsViewModels input)
        {
            var id = dbContext.Add(new Competitions
            {
                Name = input.Name,
                StartRaceDate = input.StartRaceDate,
            });
            dbContext.SaveChanges();

            for (int i = 0; i < input.CompetitionsRallyRunwayId.Count; i++)
            {
                dbContext.CompetitionsRallyRunway.Add(new CompetitionsRallyRunway
                {
                    RallyRunwayId = input.CompetitionsRallyRunwayId[i],
                    CompetitionsId = id.Entity.Id,
                });
            }
            dbContext.SaveChanges();
        }

        public void CreateRunway(RunwayViewModels input)
        {
            dbContext.RallyRunways.Add(new RallyRunway { Name = input.Name, TrackLength = input.TrackLength,
                Difficulty = input.Difficulty, Description = input.Description });
            dbContext.SaveChanges();
        }

        public void CreatePilot(PilotViewModels input)
        {
            Random rnd = new Random();
            string? firstN = input.FirstName;
            string? lastN = input.LastName;


            if (firstN == null)
            {
                int first = rnd.Next(1, 100);
                firstN = dbContext.FirstNames.Select(x => new { x.FirstName, x.Id })
               .FirstOrDefault(x => x.Id == first).FirstName;
            }
            if (lastN == null)
            {
                int last = rnd.Next(1, 100);
                lastN = dbContext.LastNames.Select(x => new { x.LastName, x.Id })
                .FirstOrDefault(x => x.Id == last).LastName;
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
            dbContext.SaveChanges();
        }

        public void CreateNavigator(NavigatorViewModels input)
        {
            Random rnd = new Random();
            string? firstN = input.FirstName;
            string? lastN = input.LastName;

            if (firstN == null)
            {
                int first = rnd.Next(1, 100);
                firstN = dbContext.FirstNames.Select(x => new { x.FirstName, x.Id })
               .FirstOrDefault(x => x.Id == first).FirstName;
            }
            if (lastN == null)
            {
                int last = rnd.Next(10, 100);
                lastN = dbContext.LastNames.Select(x => new { x.LastName, x.Id })
                .FirstOrDefault(x => x.Id == last).LastName;
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
            dbContext.SaveChanges();
        }

        public void CreateParts(PartsViewModels input)
        {
            dbContext.Add(new PartsCars
            {
                Type = input.Type,
                Name = input.Name,
                Price = input.Price,
                Strength = input.Strength,
                Speed = input.Speed,
            });
            dbContext.SaveChanges();
        }
    }
}

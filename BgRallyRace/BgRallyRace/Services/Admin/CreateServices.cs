namespace BgRallyRace.Services.Admin
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.Models.Competitions;
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
        
        public string CreateCompetitions(CompetitionsViewModels input)
        {
            var id = dbContext.Add(new Competitions
            {
                Name = input.Name,
                StartRaceDate = input.StartRaceDate,
                Stages = input.Stages,
                PrizeFund = input.PrizeFund,
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
            return "Състезянието е успешно създадено.";
        }

        public string CreateRunway(RunwayViewModels input)
        {
            dbContext.RallyRunways.Add(new RallyRunway { Name = input.NameRunway, TrackLength = input.TrackLength,
                Difficulty = input.Difficulty, Description = input.Description });
            dbContext.SaveChanges();
            return "Пистата е успешно създадено.";

        }

        public string CreatePilot(PilotViewModels input)
        {
            string? firstN = input.FirstName;
            string? lastN = input.LastName;


            if (firstN == null)
            {
                firstN = GeneratingFirstName();
              
            }
            if (lastN == null)
            {
                lastN = GeneratingLastName();
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
            return "Пилота е успешно създадено.";

        }

        public string CreateNavigator(NavigatorViewModels input)
        {
        
            string? firstN = input.FirstName;
            string? lastN = input.LastName;

            if (firstN == null)
            {
                firstN = GeneratingFirstName();
            }
            if (lastN == null)
            {
                lastN = GeneratingLastName();
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
            return "Навигатора е успешно създадено.";

        }

        public string CreateParts(PartsViewModels input)
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
            return "Часта е успешно създадено.";

        }

        private string GeneratingFirstName()
        {
            Random rnd = new Random();
            int first = rnd.Next(1, 100);
            var name = dbContext.FirstNames.Select(x => new { x.FirstName, x.Id })
           .FirstOrDefault(x => x.Id == first).FirstName;
            return name;
        }

        private string GeneratingLastName()
        {
            Random rnd = new Random();
            int last = rnd.Next(1, 100);
            var name = dbContext.LastNames.Select(x => new { x.LastName, x.Id })
            .FirstOrDefault(x => x.Id == last).LastName;
            return name;
        }
    }
}

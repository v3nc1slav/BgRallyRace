namespace BgRallyRace.Services.Create
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.Models.Enums;
    using System;
    using System.Linq;

    public class CreateServices : ICreateServices
    {
        private readonly ApplicationDbContext dbContext;
        public CreateServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateRunwayServices(string name, decimal length, DifficultyType difficulty, string description)
        {
            dbContext.RallyRunways.Add(new RallyRunway { Name = name, TrackLength = length, Difficulty = difficulty, Description = description });
            dbContext.SaveChanges();
        }

        public void CreatePilotServices(string? firstName, string? lastName, int age, int concentration, int experience,
            int energy, int devotion, int physicalTraining, int pounds, int salary, int reflexes)
        {
            Random rnd = new Random();
            string? firstN = firstName;
            string? lastN = lastName;

            if (firstN == null)
            {
                int first = rnd.Next(11, 112);
                firstN = dbContext.FirstNames.Select(x => new { x.FirstName, x.Id })
               .FirstOrDefault(x => x.Id == first).FirstName;
            }
            if (lastN == null)
            {
                int last = rnd.Next(11, 112);
                lastN = dbContext.LastNames.Select(x => new { x.LastName, x.Id })
                .FirstOrDefault(x => x.Id == last).LastName;
            }
     
            var rallyPilot = dbContext.RallyPilots.Add(new RallyPilots
            {
                FirstName = firstN,
                LastName = lastN,
                Age = age,
                Salary = salary,
                Concentration = concentration,
                Experience = experience,
                Energy = energy,
                Devotion = devotion,
                PhysicalTraining = physicalTraining,
                Reflexes = reflexes,
                Pounds = pounds,
            });
            dbContext.SaveChanges();
        }
    }
}

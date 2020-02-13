using BgRallyRace.Data;
using BgRallyRace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
    public class RallyPilotsServices : IRallyPilotsServices
    {
        private readonly ApplicationDbContext dbContext;

        public RallyPilotsServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public ICollection<RallyPilots>? GetPilots(string user)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            var pilot = dbContext.Teams.Where(t => t.User == user).Select(x => x.RallyPilot).ToList();
            return pilot;
        }

        public async Task<int> CreateRallyPilotsAsync()
        {
            Random rnd = new Random();
            //ToDo
            int first = rnd.Next(1, 4);
            int last = rnd.Next(1, 4);
            var firstName = await dbContext.FirstNames.Select(x => new { x.FirstName, x.Id })
               .FirstOrDefaultAsync(x => x.Id == first);
            var lastName = await dbContext.LastNames.Select(x => new { x.LastName, x.Id })
                .FirstOrDefaultAsync(x => x.Id == last);
            var rallyPilot = await dbContext.RallyPilots.AddAsync(new RallyPilots
            {
                FirstName = firstName.FirstName,
                LastName = lastName.LastName,
                Age = 18,
                Salary = 610,
                Concentration = 5,
                Experience = 5,
                Energy = 100,
                Devotion = 5,
                PhysicalTraining = 5,
                Reflexes = 5,
                Pounds = 80,
            });
            await dbContext.SaveChangesAsync();
            var id = rallyPilot.Entity.Id;
            return id;
        }

        public async Task Fired(int id)
        {
            var person = this.GetPilot(id);
            person.Result.TeamId = null;
            await dbContext.SaveChangesAsync();
        }

        public async Task IncreaseAge(int id)
        {
            var person = this.GetPilot(id);
            person.Result.Age = person.Result.Age + 1;
            await dbContext.SaveChangesAsync();
        }

        public async Task IncreaseConcentration(int id, int variable)
        {
            var person = this.GetPilot(id);
            person.Result.Concentration = person.Result.Concentration + variable;
            await dbContext.SaveChangesAsync();
        }

        public async Task DecreaseConcentration(int id, int variable)
        {
            var person = this.GetPilot(id);
            person.Result.Concentration = person.Result.Concentration - variable;
            await dbContext.SaveChangesAsync();
        }

        public async Task IncreaseDevotion(int id, int variable)
        {
            var person = this.GetPilot(id);
            person.Result.Devotion = person.Result.Devotion + variable;
            await dbContext.SaveChangesAsync();
        }

        public async Task DecreaseDevotion(int id, int variable)
        {
            var person = this.GetPilot(id);
            person.Result.Devotion = person.Result.Devotion - variable;
            await dbContext.SaveChangesAsync();
        }

        public async Task IncreaseEnergy(int id, int variable)
        {
            var person = this.GetPilot(id);
            person.Result.Energy = person.Result.Energy + variable;
            await dbContext.SaveChangesAsync();
        }

        public async Task DecreaseEnergy(int id, int variable)
        {
            var person = this.GetPilot(id);
            person.Result.Energy = person.Result.Energy - variable;
            await dbContext.SaveChangesAsync();
        }
        public async Task IncreaseExperience(int id, int variable)
        {
            var person = this.GetPilot(id);
            person.Result.Experience = person.Result.Experience + variable;
            await dbContext.SaveChangesAsync();
        }

        public async Task DecreaseExperience(int id, int variable)
        {
            var person = this.GetPilot(id);
            person.Result.Experience = person.Result.Experience - variable;
            await dbContext.SaveChangesAsync();
        }

        public async Task IncreasePhysicalTraining(int id, int variable)
        {
            var person = this.GetPilot(id);
            person.Result.PhysicalTraining = person.Result.PhysicalTraining + variable;
            await dbContext.SaveChangesAsync();
        }

        public async Task DecreasePhysicalTraining(int id, int variable)
        {
            var person = this.GetPilot(id);
            person.Result.PhysicalTraining = person.Result.PhysicalTraining - variable;
            await dbContext.SaveChangesAsync();
        }
        public async Task IncreasePounds(int id, int variable)
        {
            var person = this.GetPilot(id);
            person.Result.Pounds = person.Result.Pounds + variable;
            await dbContext.SaveChangesAsync();
        }

        public async Task DecreasePounds(int id, int variable)
        {
            var person = this.GetPilot(id);
            person.Result.Pounds = person.Result.Pounds - variable;
            await dbContext.SaveChangesAsync();
        }

        public async Task IncreaseSalary(int id, int variable)
        {
            var person = this.GetPilot(id);
            person.Result.Salary = person.Result.Salary + variable;
            await dbContext.SaveChangesAsync();
        }

        public async Task DecreaseSalary(int id, int variable)
        {
            var person = this.GetPilot(id);
            person.Result.Salary = person.Result.Salary - variable;
            await dbContext.SaveChangesAsync();
        }

        public async Task IncreaseReflexes(int id, int variable)
        {
            var person = this.GetPilot(id);
            person.Result.Reflexes = person.Result.Reflexes + variable;
            await dbContext.SaveChangesAsync();
        }

        public async Task DecreaseReflexes(int id, int variable)
        {
            var person = this.GetPilot(id);
            person.Result.Reflexes = person.Result.Reflexes - variable;
            await dbContext.SaveChangesAsync();
        }
        public async Task<RallyPilots> GetPilot(int id)
        {
            var pilot = await dbContext.RallyPilots.Where(x => x.Id == id).FirstOrDefaultAsync();
            return pilot;
        }

    }
}

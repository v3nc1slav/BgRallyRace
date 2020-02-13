using BgRallyRace.Data;
using BgRallyRace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
    public class RallyNavigatorsServices : IRallyNavigatorsServices
    {
        private readonly ApplicationDbContext dbContext;
        public RallyNavigatorsServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public RallyNavigators? GetNavigator(int id)
        {
            var navigator = dbContext.RallyNavigators.Where(x => x.Id == id).FirstOrDefault();
            return navigator;
        }

        public async Task<int> CreateRallyNavigatorsAsync()
        {
            Random rnd = new Random();
            //ToDo
            int first = rnd.Next(1, 4);
            int last = rnd.Next(1, 4);
            var  firstName = await dbContext.FirstNames.Select(x=>new { x.FirstName, x.Id })
                .FirstOrDefaultAsync(x => x.Id == first);
            var lastName = await dbContext.LastNames.Select(x => new { x.LastName, x.Id })
                .FirstOrDefaultAsync(x => x.Id == last);
           var rallyNavigator =  await dbContext.RallyNavigators.AddAsync(new RallyNavigators
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
                Communication = 5,
                Pounds = 80,
            });

            await dbContext.SaveChangesAsync();
            var id = rallyNavigator.Entity.Id;
            return id;
        }
        public Task Fired(int id)
        {
            throw new NotImplementedException();
        }

        public Task IncreaseCommunication(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public Task DecreaseCommunication(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public Task<RallyNavigators> GetNavigators(int id)
        {
            throw new NotImplementedException();
        }

        public Task IncreaseAge(int id)
        {
            throw new NotImplementedException();
        }

        public Task IncreaseConcentration(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public Task DecreaseConcentration(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public Task IncreaseDevotion(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public Task DecreaseDevotion(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public Task IncreaseEnergy(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public Task DecreaseEnergy(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public Task IncreaseExperience(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public Task DecreaseExperience(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public Task IncreasePhysicalTraining(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public Task DecreasePhysicalTraining(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public Task IncreasePounds(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public Task DecreasePounds(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public Task IncreaseSalary(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public Task DecreaseSalary(int id, int variable)
        {
            throw new NotImplementedException();
        }
    }
}


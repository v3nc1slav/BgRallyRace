using BgRallyRace.Data;
using BgRallyRace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
    public class RallyNavigatorsServices
    {
        private readonly ApplicationDbContext dbContext;

        public RallyNavigatorsServices()
        {

        }
        public RallyNavigatorsServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> CreateRallyNavigatorsAsync()
        {
            Random rnd = new Random();
            int first = rnd.Next(1, 98);
            int last = rnd.Next(1, 100);
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
                Attachment = 5,
                PhysicalTraining = 5,
                Communication = 5
            });

            await dbContext.SaveChangesAsync();
            var id = rallyNavigator.Entity.Id;
            return id;
        }
    }
}


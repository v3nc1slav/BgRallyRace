using BgRallyRace.Data;
using BgRallyRace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
    public class RallyPilotsServices
    {
        private readonly ApplicationDbContext dbContext;

        public RallyPilotsServices()
        {

        }
        public RallyPilotsServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
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
                Attachment = 5,
                PhysicalTraining = 5,
                Reflexes = 5,  
            }) ;
            await  dbContext.SaveChangesAsync();
            var id = rallyPilot.Entity.Id;
            return id;
        }
    }
}

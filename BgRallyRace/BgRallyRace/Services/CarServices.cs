using BgRallyRace.Data;
using BgRallyRace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
    public class CarServices
    {
        private readonly ApplicationDbContext dbContext;

        public CarServices()
        {

        }
        public CarServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Cars> GetCars()
        {
            var car =  dbContext.Cars.ToList();
            return car;
        }

        public async Task GetNewEngine()
        {
            var engine = await dbContext.Cars.FirstOrDefaultAsync(x => x.Id == 23);
            await dbContext.SaveChangesAsync();
        }
        public async Task<int> CreateCarsAsync()
        {
            var aerodynamics = await dbContext.Aerodynamics.FirstOrDefaultAsync(x => x.Id == 1);
            var brakes = await dbContext.Brakes.FirstOrDefaultAsync(x => x.Id == 1);
            var engines = await dbContext.Engines.FirstOrDefaultAsync(x => x.Id == 1);
            var gearboxs = await dbContext.Gearboxs.FirstOrDefaultAsync(x => x.Id == 1);
            var model = await dbContext.ModelsCars.FirstOrDefaultAsync(x => x.Id == 1);
            var mountings = await dbContext.Mountings.FirstOrDefaultAsync(x => x.Id == 1);
            var car = await dbContext.Cars.AddAsync(new Cars
            {
                Aerodynamics = aerodynamics,
                Brakes = brakes,
                Engine = engines,
                Gearbox = gearboxs,
                ModelCar = model,
                Mounting = mountings,
            });
            dbContext.SaveChanges();
            var id = car.Entity.Id;
            return id;
        }
    }
}

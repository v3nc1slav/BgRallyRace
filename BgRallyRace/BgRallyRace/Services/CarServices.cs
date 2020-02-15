using BgRallyRace.Data;
using BgRallyRace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
    public class CarServices : ICarServices
    {
        private readonly ApplicationDbContext dbContext;
        public CarServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Cars>> GetCars(string user)
        {
            var car = await dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars).ToListAsync();
            return car;
        }

        public async Task<Engines> GetCar(string user)
        {
            var car = await dbContext.Teams.Where(x => x.User == user).Select(c=>c.Cars.Engine).FirstOrDefaultAsync();
            return car;
        }

        public async Task <Cars> GetCar(int id)
        {
            var car = await dbContext.Cars.Where(x => x.Id == id).FirstOrDefaultAsync();
            return car;
        }

        public async Task GetNewEngine(string user)
        {
            var engine = await dbContext.Cars.FirstOrDefaultAsync(x => x.Team.User == user);
            engine.Engine.Speed = 100;
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

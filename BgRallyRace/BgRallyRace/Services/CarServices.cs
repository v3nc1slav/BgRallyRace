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
        const string name = "ВАЗ 2101";
        const decimal price = 100;
        const decimal strength = 100;

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

        public async Task<Engines> GetEngine(string user)
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
            var aerodynamics = await dbContext.Aerodynamics.AddAsync(new Aerodynamics { Name= name, Price = price, Strength = strength, Speed = 10, CarId= null });
            dbContext.SaveChanges(); 
            var brakes = await dbContext.Brakes.AddAsync(new Brakes { Name = name, Price = price, Strength = strength, Speed = 17, CarId = null });
            var engines = await dbContext.Engines.AddAsync(new Engines { Name = name, Price = price, Strength = strength, Speed = 45, CarId = null });
            var gearboxs = await dbContext.Gearboxs.AddAsync(new Gearboxs { Name = name, Price = price, Strength = strength, Speed = 30, CarId = null });
            var model = await dbContext.ModelsCars.AddAsync(new ModelsCars { Name = name, Price = price, Strength = strength, Speed = 15, CarId = null });
            var mountings = await dbContext.Mountings.AddAsync(new Mountings { Name = name, Price = price, Strength = strength, Speed = 25, CarId = null });
            dbContext.SaveChanges();
            var car = await dbContext.Cars.AddAsync(new Cars
            {
                AerodynamicsId = aerodynamics.Entity.Id,
                BrakesId = brakes.Entity.Id,
                EngineId = engines.Entity.Id,
                GearboxId = gearboxs.Entity.Id,
                ModelCarId = model.Entity.Id,
                MountingId = mountings.Entity.Id,
            });
            dbContext.SaveChanges();
            var id = car.Entity.Id;
            return id;
        }
    }
}

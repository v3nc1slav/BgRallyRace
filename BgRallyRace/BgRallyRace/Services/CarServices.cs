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

        public async Task<int> CreateCarsAsync()
        {
            var aerodynamics = await dbContext.aerodynamics.Select(x => new { x.Id, x.Name, x.Price, x.Speed, x.Strength })
             .FirstOrDefaultAsync(x => x.Id == 1);
            var brakes = await dbContext.Brakes.Select(x => new { x.Id, x.Name, x.Price, x.Speed, x.Strength })
              .FirstOrDefaultAsync(x => x.Id == 1);
            var engines = await dbContext.Engines.Select(x => new { x.Id, x.Name, x.Price, x.Speed, x.Strength })
              .FirstOrDefaultAsync(x => x.Id == 1);
            var gearboxs = await dbContext.Gearboxs.Select(x => new { x.Id, x.Name, x.Price, x.Speed, x.Strength })
            .FirstOrDefaultAsync(x => x.Id == 1);
            var model = await dbContext.ModelsCars.Select(x => new { x.Id, x.Name, x.Price, x.Speed, x.Strength })
              .FirstOrDefaultAsync(x => x.Id == 1);
            var mountings = await dbContext.Mountings.Select(x => new { x.Id, x.Name, x.Price, x.Speed, x.Strength })
            .FirstOrDefaultAsync(x => x.Id == 1);
            await dbContext.Cars.AddAsync(new Cars
            {
                Aerodynamics = new Aerodynamics { Name = aerodynamics.Name, Price = aerodynamics.Price, Speed = aerodynamics.Speed, Strength = aerodynamics.Strength },
                Brakes = new Brakes { Name = brakes.Name, Price = brakes.Price, Speed = brakes.Speed, Strength = brakes.Strength },
                Engine = new Engines { Name = engines.Name, Price = engines.Price, Speed = engines.Speed, Strength = engines.Strength },
                Gearbox = new Gearboxs { Name = gearboxs.Name, Price = gearboxs.Price, Speed = gearboxs.Speed, Strength = gearboxs.Strength },
                ModelCar = new ModelsCars { Name = model.Name, Price = model.Price, Speed = model.Speed, Strength = model.Strength },
                Mounting = new Mountings { Name = mountings.Name, Price = mountings.Price, Speed = mountings.Speed, Strength = mountings.Strength },
                Turbo = new Turbo { Name = "va", Price=0, Speed=0, Strength =0},
                TeamId = 1
            });
            await dbContext.SaveChangesAsync();
            var id = dbContext.Cars.Select(x => x.Id);
            return int.Parse(id.ToString());
        }
    }
}

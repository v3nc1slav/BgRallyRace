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

        public List<Cars> GetCars(string user)
        {
            var car =  dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars).ToList();
            return car;
        }

        public Engines GetEngine(int id)
        {
            var car = dbContext.Cars.Where(x => x.Id == id).Select(x => x.Engine).First();
            return car;
        }

        public Cars GetCar(int id)
        {
            var car =  dbContext.Cars.Where(x => x.Id == id).FirstOrDefault();
            return car;
        }

        public void GetNewEngine(string user)
        {
            var engine =  dbContext.Cars.FirstOrDefault(x => x.Team.User == user);
            engine.Engine.Speed = 100;
             dbContext.SaveChanges();
        }
        public int CreateCarsAsync()
        {
            var aerodynamics =  dbContext.Aerodynamics.Add(new Aerodynamics { Name= name, Price = price, Strength = strength, Speed = 10 });
            var brakes =  dbContext.Brakes.Add(new Brakes { Name = name, Price = price, Strength = strength, Speed = 17});
            var engines =  dbContext.Engines.Add(new Engines { Name = name, Price = price, Strength = strength, Speed = 45 });
            var gearboxs =  dbContext.Gearboxs.Add(new Gearboxs { Name = name, Price = price, Strength = strength, Speed = 30});
            var model =  dbContext.ModelsCars.Add(new ModelsCars { Name = name, Price = price, Strength = strength, Speed = 15});
            var mountings =  dbContext.Mountings.Add(new Mountings { Name = name, Price = price, Strength = strength, Speed = 25});
            dbContext.SaveChanges();
            var car =  dbContext.Cars.Add(new Cars
            {
                Aerodynamics  = aerodynamics.Entity,
                Brakes = brakes.Entity,
                Engine = engines.Entity,
                Gearbox = gearboxs.Entity,
                ModelCar = model.Entity,
                Mounting = mountings.Entity,
            });
            dbContext.SaveChanges();
            var id = car.Entity.Id;
            return id;
        }
    }
}

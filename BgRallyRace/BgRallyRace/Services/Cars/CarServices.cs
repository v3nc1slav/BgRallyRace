﻿namespace BgRallyRace.Services
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.Models.PartsCar;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CarServices : ICarServices
    {
        const string name = "ВАЗ 2101";
        const decimal price = 100;
        const decimal strength = 100;

        private readonly ApplicationDbContext dbContext;
        private readonly IRallyPilotsServices rallyPilots;
        private readonly IRallyNavigatorsServices rallyNavigators;
        private readonly IMoneyAccountServices money;

        public CarServices(ApplicationDbContext dbContext, IRallyPilotsServices rallyPilots,
            IRallyNavigatorsServices rallyNavigators, IMoneyAccountServices accountServices)
        {
            this.dbContext = dbContext;
            this.rallyPilots = rallyPilots;
            this.rallyNavigators = rallyNavigators;
            this.money = accountServices;
        }
        public int GetCarId(string user)
        {
            var carId = dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars.Id).FirstOrDefault();
            return carId;
        }
        public List<Cars> GetCars(string user)
        {
            var cars = dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars).ToList();
            return cars;
        }
        public Cars GetCar(string user)
        {
            var car = dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars).FirstOrDefault();
            return car;
        }
        public Cars GetCar(int id)
        {
            var car = dbContext.Cars.Where(x => x.Id == id).FirstOrDefault();
            return car;
        }

        public Aerodynamics GetAerodynamics(string user)
        {
            var variable = dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars.Aerodynamics).First();
            return variable;
        }

        public Brakes GetBrakes(string user)
        {
            var variable = dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars.Brakes).First();
            return variable;
        }

        public Engines GetEngine(string user)
        {
            var variable = dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars.Engine).First();
            return variable;
        }

        public Gearboxs GetGearboxs(string user)
        {
            var variable = dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars.Gearbox).First();
            return variable;
        }

        public ModelsCars GetModelsCars(string user)
        {
            var variable = dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars.ModelCar).First();
            return variable;
        }

        public Mountings GetMountings(string user)
        {
            var variable = dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars.Mounting).First();
            return variable;
        }

        public Turbo? GetTurbo(string user)
        {
            var variable = dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars.Turbo).First();
            return variable;
        }

        public decimal GetMaxSpeed(string user)
        {
            var car = this.GetCar(user);
            var speed = car.Aerodynamics.Speed + car.Brakes.Speed + car.Engine.Speed + car.Gearbox.Speed + car.ModelCar.Speed + car.Mounting.Speed;
            if (!(car.Turbo == null))
            {
                speed += car.Turbo.Speed;
            }
            return speed;
        }

        public void GetNewEngine(string user)
        {
            var engine = dbContext.Cars.FirstOrDefault(x => x.Team.User == user);
            engine.Engine.Speed = 100;
            dbContext.SaveChanges();
        }

        public decimal GetCurrentSpeed(Parts parts)
        {
            var currentSpeed = parts.Strength * parts.Speed / 100;
            return currentSpeed;
        }

        public int CreateCarsAsync()
        {
            var aerodynamics = dbContext.Aerodynamics.Add(new Aerodynamics { Name = name, Price = price, Strength = strength, Speed = 10 });
            var brakes = dbContext.Brakes.Add(new Brakes { Name = name, Price = price, Strength = strength, Speed = 17 });
            var engines = dbContext.Engines.Add(new Engines { Name = name, Price = price, Strength = strength, Speed = 45 });
            var gearboxs = dbContext.Gearboxs.Add(new Gearboxs { Name = name, Price = price, Strength = strength, Speed = 30 });
            var model = dbContext.ModelsCars.Add(new ModelsCars { Name = name, Price = price, Strength = strength, Speed = 15 });
            var mountings = dbContext.Mountings.Add(new Mountings { Name = name, Price = price, Strength = strength, Speed = 25 });
            dbContext.SaveChanges();
            var car = dbContext.Cars.Add(new Cars
            {
                Aerodynamics = aerodynamics.Entity,
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

        public void Repair(string type, int id, decimal price, string user)
        {

            var team = dbContext.Teams.Where(x => x.User == user).FirstOrDefault();

            if ((team.RallyPilotId ?? 0) == 0) ;
            if ((team.RallyNavigatorId ?? 0) == 0) ;

            var idPilot = team.RallyPilotId.Value;
            var idNavigator = team.RallyNavigatorId.Value;

            var pilot = rallyPilots.IsItBusy(idPilot);
            var navigator = rallyNavigators.IsItBusy(idNavigator);

            if (!(pilot && navigator))
            {
                //ToDo
                return;
            }

            if (type == "Aerodynamics")
            {
                var typeParts = dbContext.Aerodynamics.Where(x => x.Id == id).FirstOrDefault();
                RepairParts(typeParts, rallyPilots, idPilot, rallyNavigators, idNavigator);
            }
            else if (type == "Brakes")
            {
                var typeParts = dbContext.Brakes.Where(x => x.Id == id).FirstOrDefault();
                RepairParts(typeParts, rallyPilots, idPilot, rallyNavigators, idNavigator);
            }
            else if (type == "Engines")
            {
                var typeParts = dbContext.Engines.Where(x => x.Id == id).FirstOrDefault();
                RepairParts(typeParts, rallyPilots, idPilot, rallyNavigators, idNavigator);
            }
            else if (type == "Gearboxs")
            {
                var typeParts = dbContext.Gearboxs.Where(x => x.Id == id).FirstOrDefault();
                RepairParts(typeParts, rallyPilots, idPilot, rallyNavigators, idNavigator);
            }
            else if (type == "ModelsCars")
            {
                var typeParts = dbContext.ModelsCars.Where(x => x.Id == id).FirstOrDefault();
                RepairParts(typeParts, rallyPilots, idPilot, rallyNavigators, idNavigator);
            }
            else if (type == "Mountings")
            {
                var typeParts = dbContext.Mountings.Where(x => x.Id == id).FirstOrDefault();
                RepairParts(typeParts, rallyPilots, idPilot, rallyNavigators, idNavigator);
            }
            else if (type == "Turbo")
            {
                var typeParts = dbContext.Turbos.Where(x => x.Id == id).FirstOrDefault();
                RepairParts(typeParts, rallyPilots, idPilot, rallyNavigators, idNavigator);
            }

            money.ExpenseAccountAsync(price, user);
            dbContext.SaveChanges();
        }

        private void RepairParts(Parts typeParts, IRallyPilotsServices rallyPilots, int idPilot,
            IRallyNavigatorsServices rallyNavigators, int idNavigator)
        {
            var energy = 100 - typeParts.Strength;
            typeParts.Strength = 100;
            rallyPilots.IsWorking(idPilot);
            rallyPilots.DecreaseEnergy(idPilot, int.Parse(energy.ToString()));
            rallyNavigators.IsWorking(idNavigator);
            rallyNavigators.DecreaseEnergy(idNavigator, int.Parse(energy.ToString()));
        }
    }
}

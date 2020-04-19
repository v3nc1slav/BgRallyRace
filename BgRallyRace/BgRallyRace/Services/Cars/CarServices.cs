namespace BgRallyRace.Services
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Models.PartsCar;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CarServices : ICarServices
    {
        const string name = "ВАЗ 2101";
        const decimal price = 100;
        const decimal strength = 100;
        const int stageOne = 15;
        const int stageTwo = 22;
        const int stageThree = 30;
        const Double difficultyTypeEasy = 1.1;
        const Double difficultyTypeAverage = 1.3;
        const Double difficultyTypeDifficult = 1.5;

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

        public Aerodynamics GetAerodynamics(int id)
        {
            var variable = dbContext.Cars.Where(x => x.AerodynamicsId == id).Select(x => x.Aerodynamics).First();
            return variable;
        }
        public Brakes GetBrakes(string user)
        {
            var variable = dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars.Brakes).First();
            return variable;
        }

        public Brakes GetBrakes(int id)
        {
            var variable = dbContext.Cars.Where(x => x.BrakesId == id).Select(x => x.Brakes).First();
            return variable;
        }

        public Engines GetEngine(string user)
        {
            var variable = dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars.Engine).First();
            return variable;
        }

        public Engines GetEngine(int id)
        {
            var variable = dbContext.Cars.Where(x => x.EngineId == id).Select(x => x.Engine).First();
            return variable;
        }

        public Gearboxs GetGearboxs(string user)
        {
            var variable = dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars.Gearbox).First();
            return variable;
        }

        public Gearboxs GetGearboxs(int id)
        {
            var variable = dbContext.Cars.Where(x => x.GearboxId == id).Select(x => x.Gearbox).First();
            return variable;
        }

        public ModelsCars GetModelsCars(string user)
        {
            var variable = dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars.ModelCar).First();
            return variable;
        }

        public ModelsCars GetModelsCars(int id)
        {
            var variable = dbContext.Cars.Where(x => x.ModelCarId == id).Select(x => x.ModelCar).First();
            return variable;
        }

        public Mountings GetMountings(string user)
        {
            var variable = dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars.Mounting).First();
            return variable;
        }

        public Mountings GetMountings(int id)
        {
            var variable = dbContext.Cars.Where(x => x.MountingId == id).Select(x => x.Mounting).First();
            return variable;
        }

        public Turbo? GetTurbo(string user)
        {
            var variable = dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars.Turbo).First();
            return variable;
        }

        public Turbo GetTurboId(int? id)
        {
            var variable = dbContext.Cars.Where(x => x.TurboId == id).Select(x => x.Turbo).FirstOrDefault();
            return variable;
        }

        public decimal GetMaxSpeed(string user)
        {
            var car = this.GetCar(user);
            var speed = car.Aerodynamics.Speed + car.Brakes.Speed + car.Engine.Speed + car.Gearbox.Speed + car.ModelCar.Speed + car.Mounting.Speed;
            return speed;
        }

        public void GetNewAerodynamics(PartsCars part, Cars car)
        {
            var oldPart = GetAerodynamics(car.AerodynamicsId);
            substitution(part, oldPart);
        }

        public void GetNewBrakes(PartsCars part, Cars car)
        {
            var oldPart = GetBrakes(car.BrakesId);
            substitution(part, oldPart);
        }

        public void GetNewEngine(PartsCars part, Cars car)
        {
            var oldPart = GetEngine(car.EngineId);
            substitution(part, oldPart);
        }

        public void GetNewGearbox(PartsCars part, Cars car)
        {
            var oldPart = GetGearboxs(car.GearboxId);
            substitution(part, oldPart);
        }

        public void GetNewModelsCar(PartsCars part, Cars car)
        {
            var oldPart = GetModelsCars(car.ModelCarId);
            substitution(part, oldPart);
        }

        public void GetNewMountings(PartsCars part, Cars car)
        {
            var oldPart = GetMountings(car.MountingId);
            substitution(part, oldPart);
        }

        public void GetNewTurbo(PartsCars part, Cars car)
        {
            var oldPart = GetTurboId(car.TurboId);
            substitutionTurvo(part, oldPart, car.Id);
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

        public void Damage(int carId, int typeDamage, DifficultyType difficulty)
        {
            if (typeDamage == 1)
            {
                DamageStageOne(carId, difficulty);
            }
            else if (typeDamage == 2)
            {
                DamageStageTwo(carId, difficulty);
            }
            else if (typeDamage == 3)
            {
                DamageStageThree(carId, difficulty);
            }

        }

        private void DamageStageOne(int carId, DifficultyType difficulty)
        {
            if (difficulty == DifficultyType.Easy)
            {
                var aerodynamics = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Aerodynamics.Strength).FirstOrDefault();
                aerodynamics = aerodynamics - (int)(stageOne * difficultyTypeEasy);
                var brakes = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Brakes.Strength).FirstOrDefault();
                brakes = brakes - (int)(stageOne * difficultyTypeEasy);
                var engine = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Engine.Strength).FirstOrDefault();
                engine = engine - (int)(stageOne * difficultyTypeEasy);
                var gearbox = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Gearbox.Strength).FirstOrDefault();
                gearbox = gearbox - stageOne;
                var modelCar = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.ModelCar.Strength).FirstOrDefault();
                modelCar = modelCar - stageOne;
                var mounting = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Mounting.Strength).FirstOrDefault();
                mounting = mounting - stageOne;
                var turbo = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Turbo.Strength).FirstOrDefault();
                if (turbo != null)
                {
                    mounting = mounting - stageOne;
                }
                dbContext.SaveChanges();
            }
            else if(difficulty == DifficultyType.Average)
            {
                var aerodynamics = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Aerodynamics.Strength).FirstOrDefault();
                aerodynamics = aerodynamics - (int)(stageOne * difficultyTypeAverage);
                var brakes = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Brakes.Strength).FirstOrDefault();
                brakes = brakes - (int)(stageOne * difficultyTypeAverage);
                var engine = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Engine.Strength).FirstOrDefault();
                engine = engine - (int)(stageOne * difficultyTypeAverage);
                var gearbox = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Gearbox.Strength).FirstOrDefault();
                gearbox = gearbox - (int)(stageOne * difficultyTypeAverage);
                var modelCar = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.ModelCar.Strength).FirstOrDefault();
                modelCar = modelCar - stageOne;
                var mounting = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Mounting.Strength).FirstOrDefault();
                mounting = mounting - stageOne;
                var turbo = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Turbo.Strength).FirstOrDefault();
                if (turbo != null)
                {
                    mounting = mounting - stageOne;
                }
                dbContext.SaveChanges();
            }
            else if (difficulty == DifficultyType.Difficult)
            {
                var aerodynamics = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Aerodynamics.Strength).FirstOrDefault();
                aerodynamics = aerodynamics - (int)(stageOne * difficultyTypeDifficult);
                var brakes = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Brakes.Strength).FirstOrDefault();
                brakes = brakes - (int)(stageOne * difficultyTypeDifficult);
                var engine = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Engine.Strength).FirstOrDefault();
                engine = engine - (int)(stageOne * difficultyTypeDifficult);
                var gearbox = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Gearbox.Strength).FirstOrDefault();
                gearbox = gearbox - (int)(stageOne * difficultyTypeDifficult);
                var modelCar = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.ModelCar.Strength).FirstOrDefault();
                modelCar = modelCar - stageOne;
                var mounting = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Mounting.Strength).FirstOrDefault();
                mounting = mounting - (int)(stageOne * difficultyTypeDifficult);
                var turbo = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Turbo.Strength).FirstOrDefault();
                if (turbo != null)
                {
                    mounting = mounting - stageOne;
                }
                dbContext.SaveChanges();
            }
        }

        private void DamageStageTwo(int carId, DifficultyType difficulty)
        {
            if (difficulty == DifficultyType.Easy)
            {
                var aerodynamics = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Aerodynamics.Strength).FirstOrDefault();
                aerodynamics = aerodynamics - (int)(stageTwo * difficultyTypeEasy);
                var brakes = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Brakes.Strength).FirstOrDefault();
                brakes = brakes - (int)(stageTwo * difficultyTypeEasy);
                var engine = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Engine.Strength).FirstOrDefault();
                engine = engine - (int)(stageTwo * difficultyTypeEasy);
                var gearbox = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Gearbox.Strength).FirstOrDefault();
                gearbox = gearbox - stageTwo;
                var modelCar = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.ModelCar.Strength).FirstOrDefault();
                modelCar = modelCar - stageTwo;
                var mounting = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Mounting.Strength).FirstOrDefault();
                mounting = mounting - stageTwo;
                var turbo = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Turbo.Strength).FirstOrDefault();
                if (turbo != null)
                {
                    mounting = mounting - stageTwo;
                }
                dbContext.SaveChanges();
            }
            else if (difficulty == DifficultyType.Average)
            {
                var aerodynamics = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Aerodynamics.Strength).FirstOrDefault();
                aerodynamics = aerodynamics - (int)(stageTwo * difficultyTypeAverage);
                var brakes = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Brakes.Strength).FirstOrDefault();
                brakes = brakes - (int)(stageTwo * difficultyTypeAverage);
                var engine = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Engine.Strength).FirstOrDefault();
                engine = engine - (int)(stageTwo * difficultyTypeAverage);
                var gearbox = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Gearbox.Strength).FirstOrDefault();
                gearbox = gearbox - (int)(stageTwo * difficultyTypeAverage);
                var modelCar = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.ModelCar.Strength).FirstOrDefault();
                modelCar = modelCar - stageTwo;
                var mounting = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Mounting.Strength).FirstOrDefault();
                mounting = mounting - stageTwo;
                var turbo = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Turbo.Strength).FirstOrDefault();
                if (turbo != null)
                {
                    mounting = mounting - stageTwo;
                }
                dbContext.SaveChanges();
            }
            else if (difficulty == DifficultyType.Difficult)
            {
                var aerodynamics = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Aerodynamics.Strength).FirstOrDefault();
                aerodynamics = aerodynamics - (int)(stageTwo * difficultyTypeDifficult);
                var brakes = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Brakes.Strength).FirstOrDefault();
                brakes = brakes - (int)(stageTwo * difficultyTypeDifficult);
                var engine = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Engine.Strength).FirstOrDefault();
                engine = engine - (int)(stageTwo * difficultyTypeDifficult);
                var gearbox = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Gearbox.Strength).FirstOrDefault();
                gearbox = gearbox - (int)(stageTwo * difficultyTypeDifficult);
                var modelCar = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.ModelCar.Strength).FirstOrDefault();
                modelCar = modelCar - stageTwo;
                var mounting = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Mounting.Strength).FirstOrDefault();
                mounting = mounting - (int)(stageTwo * difficultyTypeDifficult);
                var turbo = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Turbo.Strength).FirstOrDefault();
                if (turbo != null)
                {
                    mounting = mounting - stageTwo;
                }
                dbContext.SaveChanges();
            }
        }

        private void DamageStageThree(int carId, DifficultyType difficulty)
        {
            if (difficulty == DifficultyType.Easy)
            {
                var aerodynamics = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Aerodynamics.Strength).FirstOrDefault();
                aerodynamics = aerodynamics - (int)(stageThree * difficultyTypeEasy);
                var brakes = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Brakes.Strength).FirstOrDefault();
                brakes = brakes - (int)(stageThree * difficultyTypeEasy);
                var engine = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Engine.Strength).FirstOrDefault();
                engine = engine - (int)(stageThree * difficultyTypeEasy);
                var gearbox = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Gearbox.Strength).FirstOrDefault();
                gearbox = gearbox - stageThree;
                var modelCar = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.ModelCar.Strength).FirstOrDefault();
                modelCar = modelCar - stageThree;
                var mounting = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Mounting.Strength).FirstOrDefault();
                mounting = mounting - stageThree;
                var turbo = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Turbo.Strength).FirstOrDefault();
                if (turbo != null)
                {
                    mounting = mounting - stageThree;
                }
                dbContext.SaveChanges();
            }
            else if (difficulty == DifficultyType.Average)
            {
                var aerodynamics = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Aerodynamics.Strength).FirstOrDefault();
                aerodynamics = aerodynamics - (int)(stageThree * difficultyTypeAverage);
                var brakes = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Brakes.Strength).FirstOrDefault();
                brakes = brakes - (int)(stageThree * difficultyTypeAverage);
                var engine = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Engine.Strength).FirstOrDefault();
                engine = engine - (int)(stageThree * difficultyTypeAverage);
                var gearbox = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Gearbox.Strength).FirstOrDefault();
                gearbox = gearbox - (int)(stageThree * difficultyTypeAverage);
                var modelCar = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.ModelCar.Strength).FirstOrDefault();
                modelCar = modelCar - stageThree;
                var mounting = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Mounting.Strength).FirstOrDefault();
                mounting = mounting - stageThree;
                var turbo = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Turbo.Strength).FirstOrDefault();
                if (turbo != null)
                {
                    mounting = mounting - stageThree;
                }
                dbContext.SaveChanges();
            }
            else if (difficulty == DifficultyType.Difficult)
            {
                var aerodynamics = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Aerodynamics.Strength).FirstOrDefault();
                aerodynamics = aerodynamics - (int)(stageThree * difficultyTypeDifficult);
                var brakes = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Brakes.Strength).FirstOrDefault();
                brakes = brakes - (int)(stageThree * difficultyTypeDifficult);
                var engine = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Engine.Strength).FirstOrDefault();
                engine = engine - (int)(stageThree * difficultyTypeDifficult);
                var gearbox = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Gearbox.Strength).FirstOrDefault();
                gearbox = gearbox - (int)(stageThree * difficultyTypeDifficult);
                var modelCar = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.ModelCar.Strength).FirstOrDefault();
                modelCar = modelCar - stageThree;
                var mounting = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Mounting.Strength).FirstOrDefault();
                mounting = mounting - (int)(stageThree * difficultyTypeDifficult);
                var turbo = dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Turbo.Strength).FirstOrDefault();
                if (turbo != null)
                {
                    mounting = mounting - stageThree;
                }
                dbContext.SaveChanges();
            }
        }

        private void RepairParts(Parts typeParts, IRallyPilotsServices rallyPilots, int idPilot,
            IRallyNavigatorsServices rallyNavigators, int idNavigator)
        {
            var energy = 100 - typeParts.Strength;
            typeParts.Strength = 100;
            rallyPilots.IsWorking(idPilot);
            rallyPilots.DecreaseEnergy(idPilot, Decimal.ToInt32(energy));
            rallyNavigators.IsWorking(idNavigator);
            rallyNavigators.DecreaseEnergy(idNavigator, Decimal.ToInt32(energy));
        }

        private void substitution(PartsCars newPart, Parts oldPart)
        {
            oldPart.Name = newPart.Name;
            oldPart.Price = newPart.Price;
            oldPart.Strength = newPart.Strength;
            oldPart.Speed = newPart.Speed;
        }

        private void substitutionTurvo(PartsCars newPart, Parts oldPart, int id)
        {
            if (oldPart == null)
            {
                var newTurbo = dbContext.Turbos.Add(new Turbo
                {
                    CarId = id,
                    Name = newPart.Name,
                    Price = newPart.Price,
                    Strength = newPart.Strength,
                    Speed = newPart.Speed,
                });
                dbContext.SaveChanges();
                var car = GetCar(id);
                car.TurboId = newTurbo.Entity.Id;
                dbContext.SaveChanges();
            }
            else
            {
                oldPart.Name = newPart.Name;
                oldPart.Price = newPart.Price;
                oldPart.Strength = newPart.Strength;
                oldPart.Speed = newPart.Speed;
            }
        }

    }
}

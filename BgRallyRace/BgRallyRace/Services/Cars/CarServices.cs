namespace BgRallyRace.Services
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Models.PartsCar;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CarServices : ICarServices
    {
        const string name = "ВАЗ 2101";
        const decimal price = 100;
        const decimal strength = 100;
        const double difficultyTypeEasy = 1.0;
        const double difficultyTypeAverage = 1.2;
        const double difficultyTypeDifficult = 1.4;

        private readonly ApplicationDbContext dbContext;
        private readonly IRallyPilotsServices rallyPilots;
        private readonly IRallyNavigatorsServices rallyNavigators;
        private readonly IMoneyAccountServices money;


        public CarServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public CarServices(ApplicationDbContext dbContext, IRallyPilotsServices rallyPilots,
            IRallyNavigatorsServices rallyNavigators, IMoneyAccountServices accountServices)
        {
            this.dbContext = dbContext;
            this.rallyPilots = rallyPilots;
            this.rallyNavigators = rallyNavigators;
            this.money = accountServices;
        }

        public async Task<int> GetCarId(string user)
        {
            var carId = await dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars.Id).FirstOrDefaultAsync();
            return carId;
        }

        public async Task<List<Cars>> GetCars(string user)
        {
            var cars = await dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars).ToListAsync();
            return cars;
        }

        public async Task<Cars> GetCar(string user)
        {
            var car = await dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars).FirstOrDefaultAsync();
            return car;
        }
        
        public async Task<Cars> GetCar(int id)
        {
            var car = await dbContext.Cars.Where(x => x.Id == id).FirstOrDefaultAsync();
            return car;
        }
        
        public async Task<Aerodynamics> GetAerodynamics(string user)
        {
            var variable = await dbContext.Teams.Where(x => x.User == user).Select(c => c.Cars.Aerodynamics).FirstAsync();
            return variable;
        }

        public async Task <Aerodynamics> GetAerodynamics(int id)
        {
            var variable = await dbContext.Cars.Where(x => x.AerodynamicsId == id).Select(x => x.Aerodynamics).FirstAsync();
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

        public async Task<Turbo> GetTurboId(int? id)
        {
            var variable = await dbContext.Cars.Where(x => x.TurboId == id).Select(x => x.Turbo).FirstOrDefaultAsync();
            return variable;
        }

        public decimal GetMaxSpeed(string user)
        {
            var Aerodynamics = GetAerodynamics(user).Result;
            var Brakes = GetBrakes(user);
            var Engines = GetEngine(user);
            var Gearboxs = GetGearboxs(user);
            var ModelsCars = GetModelsCars(user);
            var Mountings = GetMountings(user);
            var speed = Aerodynamics.Speed + Brakes.Speed + Engines.Speed + Gearboxs.Speed + ModelsCars.Speed + Mountings.Speed;
            return speed;
        }

        public void GetNewAerodynamics(PartsCars part, Cars car)
        {
            var oldPart = GetAerodynamics(car.AerodynamicsId).Result;
            Substitution(part, oldPart).GetAwaiter();
        }

        public void GetNewBrakes(PartsCars part, Cars car)
        {
            var oldPart = GetBrakes(car.BrakesId);
            Substitution(part, oldPart).GetAwaiter();
        }

        public void GetNewEngine(PartsCars part, Cars car)
        {
            var oldPart = GetEngine(car.EngineId);
            Substitution(part, oldPart).GetAwaiter();
        }

        public void GetNewGearbox(PartsCars part, Cars car)
        {
            var oldPart = GetGearboxs(car.GearboxId);
            Substitution(part, oldPart).GetAwaiter();
        }

        public void GetNewModelsCar(PartsCars part, Cars car)
        {
            var oldPart = GetModelsCars(car.ModelCarId);
            Substitution(part, oldPart).GetAwaiter();
        }

        public void GetNewMountings(PartsCars part, Cars car)
        {
            var oldPart = GetMountings(car.MountingId);
            Substitution(part, oldPart).GetAwaiter();
        }

        public void GetNewTurbo(PartsCars part, Cars car)
        {
            var oldPart = GetTurboId(car.TurboId).Result;
            SubstitutionTurvo(part, oldPart, car.Id).GetAwaiter();
        }

        public decimal GetCurrentSpeed(Parts parts)
        {
            var currentSpeed = parts.Strength * parts.Speed / 100;
            return currentSpeed;
        }

        public decimal GetMaxCurrentSpeed(string user)
        {
            var Aerodynamics = GetAerodynamics(user).Result;
            var Brakes = GetBrakes(user);
            var Engines = GetEngine(user);
            var Gearboxs = GetGearboxs(user);
            var ModelsCars = GetModelsCars(user);
            var Mountings = GetMountings(user);
            var speed = Aerodynamics.Speed*Aerodynamics.Strength/100 + Brakes.Speed*Brakes.Strength/100
                + Engines.Speed*Engines.Strength/100 + Gearboxs.Speed*Gearboxs.Strength/100
                + ModelsCars.Speed*ModelsCars.Strength/100+ Mountings.Speed*Mountings.Strength/100;
            return speed;
        }

        public async Task<int> CreateCarsAsync()
        {
            var aerodynamics = dbContext.Aerodynamics.AddAsync(new Aerodynamics { Name = name, Price = price, Strength = strength, Speed = 10 });
            var brakes = dbContext.Brakes.AddAsync(new Brakes { Name = name, Price = price, Strength = strength, Speed = 17 });
            var engines = dbContext.Engines.AddAsync(new Engines { Name = name, Price = price, Strength = strength, Speed = 45 });
            var gearboxs = dbContext.Gearboxs.AddAsync(new Gearboxs { Name = name, Price = price, Strength = strength, Speed = 30 });
            var model = dbContext.ModelsCars.AddAsync(new ModelsCars { Name = name, Price = price, Strength = strength, Speed = 15 });
            var mountings = dbContext.Mountings.AddAsync(new Mountings { Name = name, Price = price, Strength = strength, Speed = 25 });
            await dbContext.SaveChangesAsync();
            var car = dbContext.Cars.AddAsync(new Cars
            {
                Aerodynamics = aerodynamics.GetAwaiter().GetResult().Entity,
                Brakes = brakes.GetAwaiter().GetResult().Entity,
                Engine = engines.GetAwaiter().GetResult().Entity,
                Gearbox = gearboxs.GetAwaiter().GetResult().Entity,
                ModelCar = model.GetAwaiter().GetResult().Entity,
                Mounting = mountings.GetAwaiter().GetResult().Entity,
            });
            await dbContext.SaveChangesAsync();
            var id = car.GetAwaiter().GetResult().Entity.Id;
            return id;
        }

        public async Task<string> Repair(string type, int id, decimal price, string user)
        {

            var team = await dbContext.Teams.Where(x => x.User == user).FirstOrDefaultAsync();

            if ((team.RallyPilotId ?? 0) == 0) ;
            if ((team.RallyNavigatorId ?? 0) == 0) ;

            var idPilot = team.RallyPilotId.Value;
            var idNavigator = team.RallyNavigatorId.Value;

            var pilot = rallyPilots.IsItBusy(idPilot);
            var navigator = rallyNavigators.IsItBusy(idNavigator);

            if ((pilot || navigator))
            {
                return "Пилота и навигатора извършват друга работа.";
            }

            if (type == "Aerodynamics")
            {
                var typeParts = await dbContext.Aerodynamics.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (typeParts.Strength == 100)
                {
                    return "Часта е напълно здрава.";
                }
                RepairParts(typeParts, rallyPilots, idPilot, rallyNavigators, idNavigator).GetAwaiter(); ;
            }
            else if (type == "Brakes")
            {
                var typeParts = await dbContext.Brakes.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (typeParts.Strength == 100)
                {
                    return "Часта е напълно здрава.";
                }
                RepairParts(typeParts, rallyPilots, idPilot, rallyNavigators, idNavigator).GetAwaiter(); ;
            }
            else if (type == "Engines")
            {
                var typeParts = await dbContext.Engines.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (typeParts.Strength == 100)
                {
                    return "Часта е напълно здрава.";
                }
                RepairParts(typeParts, rallyPilots, idPilot, rallyNavigators, idNavigator).GetAwaiter(); ;
            }
            else if (type == "Gearboxs")
            {
                var typeParts = await dbContext.Gearboxs.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (typeParts.Strength == 100)
                {
                    return "Часта е напълно здрава.";
                }
                RepairParts(typeParts, rallyPilots, idPilot, rallyNavigators, idNavigator).GetAwaiter(); ;
            }
            else if (type == "ModelsCars")
            {
                var typeParts = await dbContext.ModelsCars.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (typeParts.Strength == 100)
                {
                    return "Часта е напълно здрава.";
                }
                RepairParts(typeParts, rallyPilots, idPilot, rallyNavigators, idNavigator).GetAwaiter(); ;
            }
            else if (type == "Mountings")
            {
                var typeParts = await dbContext.Mountings.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (typeParts.Strength == 100)
                {
                    return "Часта е напълно здрава.";
                }
                RepairParts(typeParts, rallyPilots, idPilot, rallyNavigators, idNavigator).GetAwaiter(); ;
            }
            else if (type == "Turbo")
            {
                var typeParts = await dbContext.Turbos.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (typeParts.Strength == 100)
                {
                    return "Часта е напълно здрава.";
                }
                RepairParts(typeParts, rallyPilots, idPilot, rallyNavigators, idNavigator).GetAwaiter(); ;
            }

            money.ExpenseAccountAsync(price, user);
            await dbContext.SaveChangesAsync();
            return "Ремонта, бе извършен успешно";
        }

        public void Damage(int carId, int typeDamage, DifficultyType difficulty)
        {
            if (typeDamage == 1)
            {
                DamageStage(carId, difficulty, 1).GetAwaiter();
            }
            else if (typeDamage == 2)
            {
                DamageStage(carId, difficulty, 2).GetAwaiter();
            }
            else if (typeDamage == 3)
            {
                DamageStage(carId, difficulty, 3).GetAwaiter();
            }
        }

        private async Task DamageStage(int carId, DifficultyType difficulty, int stages)
        {
            int stage = 0;
            stage = (stages) switch
            {
                1 => 15,
                2 => 22,
                3 => 15,
                _ => 0,
            };
            if (difficulty == DifficultyType.Easy)
            {
                var aerodynamics = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Aerodynamics).FirstOrDefaultAsync();
                aerodynamics.Strength -= (int)(stage * difficultyTypeEasy);
                aerodynamics.Strength = ItIsNegative(aerodynamics);
               var brakes = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Brakes).FirstOrDefaultAsync();
                brakes.Strength -= (int)(stage * difficultyTypeEasy);
                brakes.Strength = ItIsNegative(brakes);
                var engine = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Engine).FirstOrDefaultAsync();
                engine.Strength -= (int)(stage * difficultyTypeEasy);
                engine.Strength = ItIsNegative(engine);
                var gearbox = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Gearbox).FirstOrDefaultAsync();
                gearbox.Strength -= stage;
                gearbox.Strength = ItIsNegative(gearbox);
                var modelCar = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.ModelCar).FirstOrDefaultAsync();
                modelCar.Strength -= stage;
                modelCar.Strength = ItIsNegative(modelCar);
                var mounting = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Mounting).FirstOrDefaultAsync();
                mounting.Strength -= stage;
                mounting.Strength = ItIsNegative(mounting);
                var turbo = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Turbo).FirstOrDefaultAsync();
                if (turbo != null)
                {
                    turbo.Strength -= stage;
                    turbo.Strength = ItIsNegative(turbo);
                }
                await dbContext.SaveChangesAsync();
            }
            else if (difficulty == DifficultyType.Average)
            {
                var aerodynamics = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Aerodynamics).FirstOrDefaultAsync();
                aerodynamics.Strength -= (int)(stage * difficultyTypeAverage);
                aerodynamics.Strength = ItIsNegative(aerodynamics);
                var brakes = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Brakes).FirstOrDefaultAsync();
                brakes.Strength -= (int)(stage * difficultyTypeAverage);
                brakes.Strength = ItIsNegative(brakes);
                var engine = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Engine).FirstOrDefaultAsync();
                engine.Strength -= (int)(stage * difficultyTypeAverage);
                engine.Strength = ItIsNegative(engine);
                var gearbox = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Gearbox).FirstOrDefaultAsync();
                gearbox.Strength -= (int)(stage * difficultyTypeAverage);
                gearbox.Strength = ItIsNegative(gearbox);
                var modelCar = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.ModelCar).FirstOrDefaultAsync();
                modelCar.Strength -= stage;
                modelCar.Strength = ItIsNegative(modelCar);
                var mounting = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Mounting).FirstOrDefaultAsync();
                mounting.Strength -= stage;
                mounting.Strength = ItIsNegative(mounting);
                var turbo = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Turbo).FirstOrDefaultAsync();
                if (turbo != null)
                {
                    turbo.Strength -= stage;
                    turbo.Strength = ItIsNegative(turbo);
                }
                 await dbContext.SaveChangesAsync();
            }
            else if (difficulty == DifficultyType.Difficult)
            {
                var aerodynamics = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Aerodynamics).FirstOrDefaultAsync();
                aerodynamics.Strength -=  (int)(stage * difficultyTypeDifficult);
                aerodynamics.Strength = ItIsNegative(aerodynamics);
                var brakes = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Brakes).FirstOrDefaultAsync();
                brakes.Strength -=  (int)(stage * difficultyTypeDifficult);
                brakes.Strength = ItIsNegative(brakes);
                var engine = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Engine).FirstOrDefaultAsync();
                engine.Strength -=   (int)(stage * difficultyTypeDifficult);
                engine.Strength = ItIsNegative(engine);
                var gearbox = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Gearbox).FirstOrDefaultAsync();
                gearbox.Strength -= (int)(stage * difficultyTypeDifficult);
                gearbox.Strength = ItIsNegative(gearbox);
                var modelCar = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.ModelCar).FirstOrDefaultAsync();
                modelCar.Strength -= stage;
                modelCar.Strength = ItIsNegative(modelCar);
                var mounting = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Mounting).FirstOrDefaultAsync();
                mounting.Strength -= (int)(stage * difficultyTypeDifficult);
                mounting.Strength = ItIsNegative(mounting);
                var turbo = await dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Turbo).FirstOrDefaultAsync();
                if (turbo != null)
                {
                    turbo.Strength -= stage;
                    turbo.Strength = ItIsNegative(turbo);
                }
                await dbContext.SaveChangesAsync();
            }
        }

        private decimal ItIsNegative(Parts parts)
        {
            if (parts.Strength<0)
            {
                return 0;
            }
            return parts.Strength;
        }

        private async Task RepairParts(Parts typeParts, IRallyPilotsServices rallyPilots, int idPilot,
            IRallyNavigatorsServices rallyNavigators, int idNavigator)
        {
            var energy = 100 - typeParts.Strength;
            typeParts.Strength = 100;
            rallyPilots.IsWorking(idPilot);
            rallyPilots.DecreaseEnergy(idPilot, Decimal.ToInt32(energy));
            rallyNavigators.IsWorking(idNavigator);
            rallyNavigators.DecreaseEnergy(idNavigator, Decimal.ToInt32(energy));
            await dbContext.SaveChangesAsync();
        }

        private async Task Substitution(PartsCars newPart, Parts oldPart)
        {
            oldPart.Name = newPart.Name;
            oldPart.Price = newPart.Price;
            oldPart.Strength = newPart.Strength;
            oldPart.Speed = newPart.Speed;
            await dbContext.SaveChangesAsync();
        }

        private async Task SubstitutionTurvo(PartsCars newPart, Parts oldPart, int id)
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
                await dbContext.SaveChangesAsync();
                var car = GetCar(id).Result;
                car.TurboId = newTurbo.Entity.Id;
            }
            else
            {
                oldPart.Name = newPart.Name;
                oldPart.Price = newPart.Price;
                oldPart.Strength = newPart.Strength;
                oldPart.Speed = newPart.Speed;
            }
           await dbContext.SaveChangesAsync();
        }

    }
}

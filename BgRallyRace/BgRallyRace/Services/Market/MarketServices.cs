namespace BgRallyRace.Services.Market
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class MarketServices : IMarketServices
    {
        const int pageSize = 10;
        private readonly ApplicationDbContext dbContext;
        private readonly IMoneyAccountServices money;
        private readonly ICarServices cars;

        public MarketServices(ApplicationDbContext dbContext, IMoneyAccountServices moneyAccount, ICarServices car)
        {
            this.dbContext = dbContext;
            money = moneyAccount;
            this.cars = car;
        }

        public List<RallyPilots> GetPilotsForMarket(int page = 1)
        {
            var pilots = dbContext.RallyPilots
                 .Where(x => x.TeamId == null)
                 .Skip((page - 1) * pageSize)
                 .Take(10)
                 .ToList();
            return pilots;
        }

        public List<RallyNavigators> GetNavigatorsForMarket(int page = 1)
        {
            var navigators = dbContext.RallyNavigators
                .Where(x => x.TeamId == null)
                .Skip((page - 1) * pageSize)
                .Take(10)
                .ToList();
            return navigators;
        }

        public List<PartsCars> GetPartsForMarket(int page = 1)
        {
            var parts = dbContext.PartsCars
                .Skip((page - 1) * pageSize)
                .Take(10)
                .ToList();
            return parts;
        }

        public void RentalsPilot(int id, string user, decimal expense)
        {
            var pilot = dbContext.RallyPilots.Where(x => x.Id == id).FirstOrDefault();
            var team = dbContext.Teams.Where(x => x.User == user).FirstOrDefault();
            pilot.TeamId = team.Id;
            team.RallyPilotId = id;
            money.ExpenseAccountAsync(expense, user);
        }

        public void RentalsNavigator(int id, string user, decimal expense)
        {
            var pilot = dbContext.RallyNavigators.Where(x => x.Id == id).FirstOrDefault();
            var team = dbContext.Teams.Where(x => x.User == user).FirstOrDefault();
            pilot.TeamId = team.Id;
            team.RallyNavigatorId = id;
            money.ExpenseAccountAsync(expense, user);
        }

        public void RentalsPartsForCar(int id, string user)
        {
            var part =  dbContext.PartsCars.Where(x => x.Id == id).FirstOrDefault();
            var car = dbContext.Cars.Where(x => x.Team.User == user).FirstOrDefault();
            ReplacedOldPartWhithNew(part, car);
            money.ExpenseAccountAsync(part.Price, user);
            dbContext.SaveChanges();
        }

        public int TotalPilots()
        {
            var result = dbContext.RallyPilots
              .Where(x => x.TeamId == null)
              .Count();
            return result;
        }

        public int TotalNavigators()
        {
            var result = dbContext.RallyNavigators
              .Where(x => x.TeamId == null)
              .Count();
            return result;
        }

        public int TotalParts()
        {
            var result = dbContext.PartsCars
              .Count();
            return result;
        }

        private void ReplacedOldPartWhithNew(PartsCars part, Cars car)
        {
            if (part.Type.ToString() == "Аеродинамика")
            {
                cars.GetNewAerodynamics(part, car);
            }
            else if (part.Type.ToString() == "Спирачки")
            {
                cars.GetNewBrakes(part, car);
            }
            else if (part.Type.ToString() == "Двигател")
            {
                cars.GetNewEngine(part, car);
            }
            else if(part.Type.ToString() == "СкоростнаКутия")
            {
                cars.GetNewGearbox(part, car);
            }
            else if (part.Type.ToString() == "Купе")
            {
                cars.GetNewModelsCar(part, car);
            }
            else if (part.Type.ToString() == "Шаси")
            {
                cars.GetNewMountings(part, car);
            }
            else if (part.Type.ToString() == "Турбо")
            {
                cars.GetNewTurbo(part, car);
            }
        }
    }
}

﻿namespace BgRallyRace.Services.Market
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using System.Collections.Generic;
    using System.Linq;
    public class MarketServices : IMarketServices
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMoneyAccountServices money;
        public MarketServices(ApplicationDbContext dbContext, IMoneyAccountServices moneyAccount)
        {
            this.dbContext = dbContext;
            money = moneyAccount;
        }

        public List<RallyPilots> GetPilotsForMarket(int page = 1)
        {
            var pilots = dbContext.RallyPilots
                 .Skip((page - 1) * 10)
                 .Take(10)
                 .Where(x => x.TeamId == null)
                 .ToList();
            return pilots;
        }

        public List<RallyNavigators> GetNavigatorsForMarket(int page = 1)
        {
            var navigators = dbContext.RallyNavigators
                .Skip((page - 1) * 10)
                .Take(10)
                .Where(x => x.TeamId == null)
                .ToList();
            return navigators;
        }

        public List<PartsCars> GetPartsForMarket(int page = 1)
        {
            var parts = dbContext.PartsCars
                .Skip((page - 1) * 10)
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
    }
}

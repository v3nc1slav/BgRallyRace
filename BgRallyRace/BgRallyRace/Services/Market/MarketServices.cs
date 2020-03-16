namespace BgRallyRace.Services.Market
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

        public List<RallyPilots> GetPilotsForMarket()
        {
           var pilots = dbContext.RallyPilots.Where(x => x.TeamId == null).ToList();
            return pilots;
        }

        public List<RallyNavigators> GetNavigatorsForMarket()
        {
            var navigators = dbContext.RallyNavigators.Where(x => x.TeamId == null).ToList();
            return navigators;
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
    }
}

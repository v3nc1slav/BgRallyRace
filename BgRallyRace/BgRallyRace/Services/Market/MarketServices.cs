namespace BgRallyRace.Services.Market
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using System.Collections.Generic;
    using System.Linq;
    public class MarketServices : IMarketServices
    {
        private readonly ApplicationDbContext dbContext;
        public MarketServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
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
    }
}

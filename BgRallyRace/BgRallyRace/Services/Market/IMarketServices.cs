namespace BgRallyRace.Services.Market
{
    using BgRallyRace.Models;
    using System.Collections.Generic;

    public interface IMarketServices
    {
        List<RallyPilots> GetPilotsForMarket();

        List<RallyNavigators> GetNavigatorsForMarket();

        void RentalsPilot(int id, string user, decimal expense);
    }
}

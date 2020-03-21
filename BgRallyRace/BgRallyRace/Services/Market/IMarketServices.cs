namespace BgRallyRace.Services.Market
{
    using BgRallyRace.Models;
    using System.Collections.Generic;

    public interface IMarketServices
    {
        List<RallyPilots> GetPilotsForMarket(int page = 1);

        List<RallyNavigators> GetNavigatorsForMarket(int page = 1);

        void RentalsPilot(int id, string user, decimal expense);

        void RentalsNavigator(int id, string user, decimal expense);

        int TotalPilots();

        int TotalNavigators();
    }
}

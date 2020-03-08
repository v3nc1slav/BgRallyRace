namespace BgRallyRace.Services.Money
{
    using BgRallyRace.Models.Enums;
    using System.Collections.Generic;
    public interface IFinanceServices
    {
        List<FundsType> GetFunds (string user);

        List<decimal> GetExpense(string user);

        List<decimal> GetRevenue(string user);
    }
}

namespace BgRallyRace.Services.Money
{
    using BgRallyRace.Data;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Models.Money;
    using System.Collections.Generic;
    using System.Linq;

    public class FinanceServices : IFinanceServices
    {
        private readonly ApplicationDbContext dbContext;
        public FinanceServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<FundsType> GetFunds(string user)
        {
            var funds = new List<FundsType>();
            var statistic = dbContext.MoneyAccount
                 .Where(x => x.User == user)
                 .Select(x => x.FinancialStatistics)
                 .ToList();
            for (int i = 0; i < statistic.Count; i++)
            {
                var variable = statistic[i].Select(x => x.Funds).ToList();
                for (int j = 0; j < variable.Count; j++)
                {
                    funds.Add(variable[j]);
                }
            }
            return funds;
        }
    }
}

namespace BgRallyRace.Services.Money
{
    using BgRallyRace.Data;
    using BgRallyRace.Models.Enums;
    using System;
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
                var variable = statistic[i]
                    .Select(x => x.Funds)
                    .ToList();

                for (int j = 0; j < variable.Count; j++)
                {
                    funds.Add(variable[j]);
                }
            }
            return funds;
        }

        public List<decimal> GetExpense(string user)
        {
            var expense = new List<decimal>();
            var statistic = dbContext.MoneyAccount
                 .Where(x => x.User == user)
                 .Select(x => x.FinancialStatistics)
                 .ToList();

            for (int i = 0; i < statistic.Count; i++)
            {
                var variable = statistic[i]
                    .Where(x=>x.Date.Month == DateTime.UtcNow.Month)
                    .Select(x => new { x.MoneExpense, x.Date })
                    .ToList();

                for (int j = 0; j < DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month); j++)
                {
                    for (int k = 0; k < variable.Count; k++)
                    {
                        if (int.Parse(variable[k].Date.Day.ToString()) == (j+1))
                        {
                            if ((k>0)&&(int.Parse(variable[k - 1].Date.Day.ToString()) == (j + 1)))
                            {
                                expense[j] += variable[k].MoneExpense;
                            }

                            expense.Add(variable[k].MoneExpense);

                            if ((k < variable.Count-1)&&!(int.Parse(variable[k+1].Date.Day.ToString()) == (j + 1)))
                            {
                                goto Found;
                            }
                        }
                    }
                            expense.Add((decimal)0.00);
                Found:;
                }
            }
            return expense;
        }

        public List<decimal> GetRevenue(string user)
        {
            var revenue = new List<decimal>();
            var statistic = dbContext.MoneyAccount
                 .Where(x => x.User == user)
                 .Select(x => x.FinancialStatistics)
                 .ToList();

            for (int i = 0; i < statistic.Count; i++)
            {
                var variable = statistic[i]
                    .Where(x => x.Date.Month == DateTime.UtcNow.Month)
                    .Select(x => new { x.MoneRevenue, x.Date })
                    .ToList();

                for (int j = 0; j < DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month); j++)
                {
                    for (int k = 0; k < variable.Count; k++)
                    {
                        if ((k > 0) && (int.Parse(variable[k - 1].Date.Day.ToString()) == (j + 1)))
                        {
                            revenue[j] += variable[k].MoneRevenue;
                        }

                        revenue.Add(variable[k].MoneRevenue);

                        if ((k < variable.Count - 1) && !(int.Parse(variable[k + 1].Date.Day.ToString()) == (j + 1)))
                        {
                            goto Found;
                        }
                    }
                    revenue.Add((decimal)0.00);
                Found:;
                }
            }
            return revenue;
        }

        public decimal GetTotalExpense(string user)
        {
            var expense = dbContext.MoneyAccount
                .Where(x => x.User == user)
                .Select(x => x.FinancialStatistics
                .Sum(x=>x.MoneExpense))
                .FirstOrDefault();
            return expense;
        }

        public decimal GetTotalRevenue(string user)
        {
            var revenue = dbContext.MoneyAccount
                .Where(x => x.User == user)
                .Select(x => x.FinancialStatistics
                .Sum(x => x.MoneRevenue))
                .FirstOrDefault();
            return revenue;
        }
    }
}

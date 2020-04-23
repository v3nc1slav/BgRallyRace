namespace BgRallyRace.Services
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Models.Money;
    using BgRallyRace.Services.Competitions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MoneyAccountServices : IMoneyAccountServices
    {
        private readonly ApplicationDbContext dbContext;

        public MoneyAccountServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateMoneyAccount(string user)
        {
            if (FindUserAsync(user) == null)
            {
                 dbContext.MoneyAccount.Add(new MoneyAccount { Balance = 10000, User = user });
                 dbContext.SaveChanges();
            }
        }

        public void ExpenseAccountAsync(decimal expense, string user)
        {
            var dbUser = FindUserAsync(user);
            dbUser.Balance = (dbUser.Balance-expense);
            dbUser.FinancialStatistics.Add(new FinancialStatistics {Funds = FundsType.consumption, MoneExpense = expense, Date = DateTime.UtcNow });
            dbContext.SaveChanges();
        }

        public void RevenueAccountAsync(decimal revenue, string user)
        {
            var dbUser =  FindUserAsync(user);
            dbUser.Balance = (dbUser.Balance + revenue);
            dbUser.FinancialStatistics.Add(new FinancialStatistics{Funds=FundsType.revenue, MoneRevenue = revenue, Date = DateTime.UtcNow });
            dbContext.SaveChanges();
        }

        public  MoneyAccount FindUserAsync( string user)
        {
             var dbUser =  dbContext.MoneyAccount.Where(a => a.User == user).FirstOrDefault();
            return dbUser;
        }

        public int FindIdMoneyAccountAsync(string user)
        {
            var id = dbContext.MoneyAccount.FirstOrDefault(a=> a.User == user).Id;
            return id;
        }

        public decimal GetBalanceAsync(string user)
        {
             var db =  dbContext.MoneyAccount.FirstOrDefault(a => a.User == user);
            return db.Balance;
        }

        public void DistributionOfPrizeMoney(decimal money, List<CompetitionsTeams> all, List<Team> wenners)
        {
            var teamsWinners = wenners;
            var teamsAll = all;
            var count = teamsWinners.Count;
            if (count>10)
            {
                count = 10;
            }
            for (int i = 0; i < count; i++)
            {
                decimal fond = 0;
                switch (i + 1)
                {
                    case 1:
                        fond = money*0.247524752M; //25 points, percentage of points earned
                        break;
                    case 2:
                        fond = money * 0.178217822M;
                        break;
                    case 3:
                        fond = money * 0.148514851M;
                        break;
                    case 4:
                        fond = money * 0.118811881M;
                        break;
                    case 5:
                        fond = money * 0.099009901M;
                        break;
                    case 6:
                        fond = money * 0.079207921M;
                        break;
                    case 7:
                        fond = money * 0.059405941M;
                        break;
                    case 8:
                        fond = money * 0.03960396M;
                        break;
                    case 9:
                        fond = money * 0.01980198M;
                        break;
                    case 10:
                        fond = money * 0.00990099M;
                        break;
                    default:
                        fond += 0;
                        break;
                }
                RevenueAccountAsync(fond,teamsWinners[i].User);
            }
            for (int i = 0; i < teamsAll.Count; i++)
            {
                if (!teamsWinners.Contains(teamsAll[i].Team))
                {
                    RevenueAccountAsync(1000, teamsAll[i].Team.User);
                }
            }
        }
    }
}

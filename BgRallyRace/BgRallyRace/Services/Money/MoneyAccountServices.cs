namespace BgRallyRace.Services
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Models.Money;
    using System;
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
            dbUser.FinancialStatistics.Add(new FinancialStatistics {Funds = FundsType.разход, MoneExpense = expense, Date = DateTime.UtcNow });
            dbContext.SaveChanges();
        }

        public void RevenueAccountAsync(decimal revenue, string user)
        {
            var dbUser =  FindUserAsync(user);
            dbUser.Balance = (dbUser.Balance + revenue);
            dbUser.FinancialStatistics.Add(new FinancialStatistics{Funds=FundsType.приход, MoneRevenue = revenue, Date = DateTime.UtcNow });
            dbContext.SaveChanges();
        }

        public  MoneyAccount? FindUserAsync( string user)
        {
             var dbUser =  dbContext.MoneyAccount.FirstOrDefault(a => a.User == user);
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
     
    }
}

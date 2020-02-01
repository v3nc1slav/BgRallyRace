using BgRallyRace.Data;
using BgRallyRace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
    public class MoneyAccountServices
    {
        private readonly ApplicationDbContext dbContext;

        public MoneyAccountServices()
        {

        }
        public MoneyAccountServices(ApplicationDbContext dbContext)
        {

            this.dbContext = dbContext;
        }
        public void CreateMoneyAccount(string user)
        {
            dbContext.MoneyAccount.Add(new MoneyAccount {Balance=10000, User =user});
            dbContext.SaveChangesAsync();
        }

        public void ExpenseAccount(int expense, string user)
        {
            var db = dbContext.MoneyAccount.Update(Balance, -expense).Where(x=>x.user = user);
            dbContext.SaveChangesAsync();
        }
    }
}

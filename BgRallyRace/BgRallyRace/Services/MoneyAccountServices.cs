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

        public void ExpenseAccount(decimal expense, string user)
        {
            var db = FindUser(user);
            db.Balance = (db.Balance-expense);  
            dbContext.SaveChangesAsync();
        }

        public void RevenueAccount(decimal revenue, string user)
        {
           var db = FindUser(user);
            db.Balance = (db.Balance + revenue);
            dbContext.SaveChangesAsync();
        }

        public MoneyAccount FindUser( string user)
        {
            var db = dbContext.MoneyAccount.FirstOrDefault(a => a.User == user);
            return db;
        }

        public decimal GetBalance(string user)
        {
            var db = dbContext.MoneyAccount.FirstOrDefault(a => a.User == user);
            return db.Balance;
        }
     
    }
}

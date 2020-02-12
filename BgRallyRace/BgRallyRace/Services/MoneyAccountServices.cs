﻿using BgRallyRace.Data;
using BgRallyRace.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task CreateMoneyAccount(string user)
        {
            await dbContext.MoneyAccount.AddAsync(new MoneyAccount {Balance=10000, User =user});
            await dbContext.SaveChangesAsync();
        }

        public async Task ExpenseAccountAsync(decimal expense, string user)
        {
            var dbUser = await FindUserAsync(user);
            dbUser.Balance = (dbUser.Balance-expense);
            await dbContext.SaveChangesAsync();
        }

        public async Task RevenueAccountAsync(decimal revenue, string user)
        {
           var dbUser = await FindUserAsync(user);
            dbUser.Balance = (dbUser.Balance + revenue);
            await dbContext.SaveChangesAsync();
        }

        public async Task<MoneyAccount> FindUserAsync( string user)
        {
             var dbUser = await dbContext.MoneyAccount.FirstOrDefaultAsync(a => a.User == user);
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

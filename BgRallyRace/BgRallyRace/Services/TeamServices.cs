using BgRallyRace.Data;
using BgRallyRace.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
    public class TeamServices
    {
        private readonly ApplicationDbContext dbContext;

     
        public TeamServices()
        {

        }
        public TeamServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateTeam(string text, string user)
        {
            var db = new ApplicationDbContext();
            var money = new MoneyAccountServices(db);
            money.ExpenseAccount(2000, user);
            money.CreateMoneyAccount(user);
            //dbContext.Teams.Add(new Teams {Name = text, User = user });
            dbContext.SaveChangesAsync();
        }
    }
}

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
            var money = new MoneyAccountServices(dbContext);
            money.CreateMoneyAccount(user);
            dbContext.Teams.Add(new Team {Name = text, User = user });
            dbContext.SaveChangesAsync();
        }

        public Team FindUser(string user)
        {
            var findUser = dbContext.Teams.FirstOrDefault(a => a.User == user);
            return findUser;
        }
    }
}

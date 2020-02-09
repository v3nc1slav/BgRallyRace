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

        public async Task CreateTeam(string text, string user)
        {
            var moneyId = new MoneyAccountServices(dbContext);
            var pilot = new RallyPilotsServices(dbContext);
            var navigator = new RallyNavigatorsServices(dbContext);
            var car = new CarServices(dbContext);
            moneyId.FindIdMonyeAccount(user);
            //await pilot.CreateRallyPilotsAsync();
            //await navigator.CreateRallyNavigatorsAsync();
            await car.CreateCarsAsync();
            await dbContext.Teams.AddAsync(new Team 
            {
                Name = text,
                User = user, 
                MoneyAccountId =1,//int.Parse(moneyId.ToString()),
                RallyPilotId = 1,//int.Parse(pilot.ToString()),
                RallyNavigatorId = int.Parse(navigator.ToString()),
                CarId = int.Parse(car.ToString()),
            });
            await dbContext.SaveChangesAsync();
        }

        public Team FindUser(string user)
        {
            var findUser = dbContext.Teams.FirstOrDefault(a => a.User == user);
            return findUser;
        }
    }
}

using BgRallyRace.Data;
using BgRallyRace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            var numberMoney =  moneyId.FindIdMoneyAccountAsync(user);
            var numberPilot =  pilot.CreateRallyPilotsAsync();
            var numberNavigator =  navigator.CreateRallyNavigatorsAsync();
            var numberCar =  car.CreateCarsAsync();
            var newTeam = await dbContext.Teams.AddAsync(new Team
            {
                Name = text,
                User = user,
                MoneyAccountId = numberMoney,
                RallyPilotId = numberPilot,
                RallyNavigatorId = numberNavigator,
                CarId = numberCar,
            }) ;
            dbContext.SaveChanges();
            var addCarId = car.GetCar(numberCar);
            var addPilotId = pilot.GetPilot(numberPilot);
            var addNavigatorId = navigator.GetNavigator(numberPilot);
            addCarId.TeamId = newTeam.Entity.Id;
            addPilotId.Result.TeamId = newTeam.Entity.Id;
            addNavigatorId.TeamId = newTeam.Entity.Id;
            dbContext.SaveChanges();
        }

        public async Task<Team>? FindUser(string user)
        {
            var findUser = await dbContext.Teams.FirstOrDefaultAsync(a => a.User == user);
            return findUser;
        }
    }
}

namespace BgRallyRace.Services
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    public class TeamServices : ITeamServices
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICarServices car;
        private readonly IMoneyAccountServices money;
        private readonly IRallyPilotsServices pilot;
        private readonly IRallyNavigatorsServices navigator;

        public TeamServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public TeamServices(ApplicationDbContext dbContext, ICarServices carServices, IMoneyAccountServices moneyAccountServices,
            IRallyPilotsServices rallyPilots, IRallyNavigatorsServices navigatorsServices)
        {
            this.dbContext = dbContext;
            this.car = carServices;
            this.money = moneyAccountServices;
            this.pilot = rallyPilots;
            this.navigator = navigatorsServices;
        }

        public async Task CreateTeamAsync(string text, string user)
        {
            var numberMoney =  money.FindIdMoneyAccountAsync(user);
            var numberPilot = pilot.CreateRallyPilotsAsync();
            var numberNavigator =  navigator.CreateRallyNavigatorsAsync();
            var numberCar =  car.CreateCarsAsync();
            var newTeam =  dbContext.Teams.Add(new Team
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
            var addNavigatorId = navigator.GetNavigator(numberNavigator);
            addCarId.TeamId = newTeam.Entity.Id;
            addPilotId.TeamId = newTeam.Entity.Id;
            addNavigatorId.TeamId = newTeam.Entity.Id;
            await dbContext.SaveChangesAsync();
        }

        public async Task<Team> FindUserAsync(string user)
        {
            var findUser = await dbContext.Teams.FirstOrDefaultAsync(a => a.User == user);
            return findUser;
        }

        public async Task<Team> FindUserAsync(int id)
        {
            var findUser = await dbContext.Teams.FirstOrDefaultAsync(a => a.Id == id);
            return findUser;
        }

        public async Task<int> GetTeamIdAsync(string user)
        {
            var findUser =  dbContext.Teams.FirstOrDefaultAsync(a => a.User == user).Id;
            return findUser;
        }
    }
}

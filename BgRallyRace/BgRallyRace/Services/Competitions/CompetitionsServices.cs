namespace BgRallyRace.Services.Competitions
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.Services.Runways;
    using BgRallyRace.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompetitionsServices : ICompetitionsServices
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICarServices cars;
        private readonly ITeamServices teams;
        private readonly IRallyPilotsServices pilots;
        private readonly IRallyNavigatorsServices navigators;
        private readonly IRunwaysServices runways;

        public CompetitionsServices(ApplicationDbContext dbContext, ICarServices carServices, ITeamServices teamServices,
            IRallyPilotsServices pilotsServices, IRallyNavigatorsServices navigatorsServices, IRunwaysServices runwaysServices)
        {
            this.dbContext = dbContext;
            this.cars = carServices;
            this.teams = teamServices;
            this.pilots = pilotsServices;
            this.navigators = navigatorsServices;
            this.runways = runwaysServices;
        }

        public async Task<DateTime> GetStartDate()
        {
            var date = await dbContext
                .Competitions
                .Where(x => x.StartRaceDate > DateTime.Now)
                .Select(x => x.StartRaceDate)
                .FirstOrDefaultAsync();
            return date;
        }

        public async Task<int> GetCompetitionId()
        {
            var date = await dbContext
                .Competitions
                .Where(x => x.StartRaceDate > DateTime.Now)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
            return date;
        }

        public async Task<string> GetCompetitionName()
        {
            var date = await dbContext
                .Competitions
                .Where(x => x.StartRaceDate > DateTime.Now)
                .Select(x => x.Name)
                .FirstOrDefaultAsync();
            return date;
        }

        public async Task HasIsStartedAsync()
        {
            var date = await this.GetStartDate();
            var nowDate = DateTime.Now;
            if (date == nowDate)
            {
                var car = dbContext.Competitions.First();
                car.CompetitionsRallyRunwayId = 1;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task RallyЕntry(TeamViewModels input)
        {
            var team = teams.FindUser(input.TeamId);
            dbContext.CompetitionsTeam.Add(new CompetitionsTeams
            {
                Team = input.Team,
                TeamId = input.TeamId,
                UseOfTurboType = input.UseOfTurbo,
                CompetitionId = input.CompetitionId,
                Drive = input.Drive,
                PilotId = input.RallyPilotId,
                NavigatorId = input.RallyNavigatorId,
                CarId = input.CarId,
            });
            await dbContext.SaveChangesAsync();
        }

        public void StartRalli()
        {
            var teams = GetAllTeamsAsync();
            pilots.AllPilotsNoWorking();
            navigators.AllNavigatorNoWorking();
            var runway = runways.GetRunwayForCurrentRace();//ToDo?


        }

        public async Task<CompetitionsTeams[]> GetAllTeamsAsync()
        {
            var teams = await dbContext
                .CompetitionsTeam
                .Where(x=>x.Competition.StartRaceDate == DateTime.Now.Date)
                .ToArrayAsync();
            return teams;
        }
    }
}

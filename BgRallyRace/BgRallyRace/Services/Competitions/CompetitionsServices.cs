namespace BgRallyRace.Services.Competitions
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.Models.Competitions;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Services.Runways;
    using BgRallyRace.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class CompetitionsServices : ICompetitionsServices
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICarServices cars;
        private readonly ITeamServices team;
        private readonly IRallyPilotsServices pilots;
        private readonly IRallyNavigatorsServices navigators;
        private readonly IRunwaysServices runways;
        private readonly IRaceHistoryServices raceHistory;
        private readonly IPeople people;
        private readonly ICompetitionsServices competitions;

        public CompetitionsServices(ApplicationDbContext dbContext, ICarServices carServices, ITeamServices teamServices,
            IRallyPilotsServices pilotsServices, IRallyNavigatorsServices navigatorsServices, IRunwaysServices runwaysServices,
            IRaceHistoryServices raceHistoryServices, IPeople people)
        {
            this.dbContext = dbContext;
            this.cars = carServices;
            this.team = teamServices;
            this.pilots = pilotsServices;
            this.navigators = navigatorsServices;
            this.runways = runwaysServices;
            this.raceHistory = raceHistoryServices;
            this.people = people;
        }

        public DateTime GetStartDate()
        {
            var date = dbContext
                .Competitions
                .Where(x => x.Applicable == true)
                .Select(x => x.StartRaceDate)
                .FirstOrDefault();
            return date;
        }

        public int GetCompetitionId()
        {
            var date = dbContext
                .Competitions
                .Where(x => x.Applicable == true)
                .Select(x => x.Id)
                .FirstOrDefault();
            return date;
        }

        public string GetCompetitionName()
        {
            var date = dbContext
                .Competitions
                .Where(x => x.Applicable == true)
                .Select(x => x.Name)
                .FirstOrDefault();
            return date;
        }

        public void HasIsStartedAsync()
        {
            var t = Task.Run(() =>
            {
                while (true)
                {
                    var date = GetStartDate();
                    var nowDate = DateTime.Now;
                    if (date < nowDate)
                    {
                        this.StartRalli();
                    }
                    Thread.Sleep(1000 * 60);
                }
            });

        }

        public async Task RallyЕntry(TeamViewModels input)
        {
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
            var raceName = GetCompetitionName();
            var teams = GetAllTeamsAsync();
            pilots.AllPilotsNoWorking();
            navigators.AllNavigatorNoWorking();
            var runway = runways.GetRunwayForCurrentRace();
            var stageOne = runway.TrackLength / 3;
            var stageTwo = runway.TrackLength / 3;
            var stageThree = runway.TrackLength / 3;
            var data = GetStartDate();
            string input = $"Бе дадено началото на състезанието {raceName}, провежащо се на дата: {data} " +
                $"на писта {runway.Name}.";//ToDo
            raceHistory.AddHistory(input);
            input = "Очакваме всеки момент да потегли първият автомобил.";
            raceHistory.AddHistory(input);
            var count = teams.Length;
            for (int i = 0; i < count; i++)
            {
                var pilot = pilots.GetPilotNoTracking(teams[i].PilotId);
                var energyPilot = pilots.EnergyPilot(teams[i].PilotId) - stageOne*(decimal)0.5;//constant
                people.ReduceEnergy(pilot, energyPilot);
                var navigator = navigators.GetNavigator(teams[i].NavigatorId);
                var energyNavigator = navigators.EnergyNavigator(teams[i].NavigatorId) - stageOne * (decimal)0.5;//constant
                people.ReduceEnergy(navigator, energyNavigator);
                var teamRace = team.FindUser(teams[i].TeamId);
                int drive = 0;
                if (i == 0)
                {
                    if (teams[i].Drive == DriveType.Aggressive)
                    {
                        input = $"Първи стартира отборът на {teamRace.Name}, с пилот{pilot}, който тръгва с мръсна газ. Наблюдаваме доста агресивно" +
           $" каране дано, навигатор до него {navigator} му влее малко разум.";
                        drive = 3;
                    }
                    else if (teams[i].Drive == DriveType.Normal)
                    {
                        input = $"Първи стартира отборът на {teamRace.Name}, с пилот{pilot}" +
          $" и навигатор до него {navigator} ";
                        drive = 2;
                    }
                    else
                    {
                        input = $"Първи стартира отборът на {teamRace.Name}, с пилот{pilot}, който потегля доста плахо. С това предпазливо каране, ще " +
         $"изостане доста в класацията, дано навигатор до него {navigator} му повлияе полужително.";
                        drive = 1;
                    }

                }
                else
                {
                    if (teams[i].Drive == DriveType.Aggressive)
                    {
                        input = $"Следващ е отборът на {teamRace.Name}, с пилот{pilot}, който тръгва с мръсна газ. Наблюдаваме доста агресивно" +
           $" каране дано, навигатор до него {navigator} му влее малко разум.";
                        drive = 3;
                    }
                    else if (teams[i].Drive == DriveType.Normal)
                    {
                        input = $"Следващия отборът на старта е {teamRace.Name}, с пилот{pilot}" +
          $" и навигатор до него {navigator} ";
                        drive = 2;
                    }
                    else
                    {
                        input = $"Към старт линията се приближава отборът на {teamRace.Name}, с пилот{pilot}, който потегля доста плахо. С това предпазливо каране, ще " +
         $"изостане доста в класацията, дано навигатор до него {navigator} му повлияе полужително.";
                        drive = 1;
                    }
                }

                raceHistory.AddHistory(input);

                var random = RandomEvents(pilot, navigator, runway.Difficulty, drive);

            }
        }

        public CompetitionsTeams[] GetAllTeamsAsync()
        {
            var teams = dbContext
                .CompetitionsTeam
                .Where(x => x.Competition.Applicable == true)
                .ToArray();
            return teams;
        }

        private string RandomEvents(RallyPilots pilot, RallyNavigators navigator, DifficultyType type, int drive)
        {
            var constantDifficul = 8;
            var constantAverage = 4;
            var constantEasy = 2;
            Random rnd = new Random();
            var chanceOfAnEvent = ((100 - pilot.Concentration)+ (100 - navigator.Concentration) )/ 4;
            var randomNumberForIncident = 0;
            var randomNumberForSalvation = 0;
            if (type == DifficultyType.Difficult)
            {
                if (drive == 3)
                {
                    randomNumberForIncident = constantDifficul + constantDifficul * chanceOfAnEvent / 100 + 2;
                }
                else if (drive == 2)
                {
                    randomNumberForIncident = constantDifficul + constantDifficul * chanceOfAnEvent / 100;
                }
                else if (drive == 1)
                {
                    randomNumberForIncident = constantDifficul  + constantDifficul * chanceOfAnEvent / 100 - 2;
                }
                var chanceForIncident = rnd.Next(1, randomNumberForIncident);
                if (!(chanceForIncident == 1 || chanceForIncident == 2 || chanceForIncident == 3))
                {
                    randomNumberForSalvation = (100 - pilot.Reflexes + 100-navigator.Communication)/20;
                    var chanceForSalvation = rnd.Next(1, randomNumberForSalvation);
                    if (chanceForSalvation==1 || chanceForSalvation == 2 || chanceForSalvation == 3)
                    {
                        return "4";
                    }
                    else if (chanceForSalvation == 4 || chanceForSalvation == 5 || chanceForSalvation == 6)
                    {
                        return "30";
                    }
                    else
                    {
                        return "the end";
                    }
                }

            }
            else if (type == DifficultyType.Average)
            {
                if (drive == 3)
                {
                    randomNumberForIncident = constantAverage + constantAverage * chanceOfAnEvent / 100 + 2;
                }
                else if (drive == 2)
                {
                    randomNumberForIncident = constantAverage + constantAverage * chanceOfAnEvent / 100;
                }
                else if (drive == 1)
                {
                    randomNumberForIncident = constantAverage + constantAverage * chanceOfAnEvent / 100 - 2;
                }
                var chanceForIncident = rnd.Next(1, randomNumberForIncident);
                if (!(chanceForIncident == 1 || chanceForIncident == 2 || chanceForIncident == 3))
                {
                    randomNumberForSalvation = (100 - pilot.Reflexes + 100 - navigator.Communication) / 20;
                    var chanceForSalvation = rnd.Next(1, randomNumberForSalvation);
                    if (chanceForSalvation == 1 || chanceForSalvation == 2 || chanceForSalvation == 3)
                    {
                        return "2";
                    }
                    else if (chanceForSalvation == 4 || chanceForSalvation == 5 || chanceForSalvation == 6)
                    {
                        return "15";
                    }
                    else
                    {
                        return "the end";
                    }
                }
            }
            else
            {
                if (drive == 3)
                {
                    randomNumberForIncident = constantEasy + constantEasy * chanceOfAnEvent / 100 + 2;
                }
                else if (drive == 2)
                {
                    randomNumberForIncident = constantEasy + constantEasy * chanceOfAnEvent / 100;
                }
                else if (drive == 1)
                {
                    randomNumberForIncident = constantEasy + constantEasy * chanceOfAnEvent / 100 - 2;
                }
                var chanceForIncident = rnd.Next(1, randomNumberForIncident);
                if (!(chanceForIncident == 1 || chanceForIncident == 2 || chanceForIncident == 3))
                {
                    randomNumberForSalvation = (100 - pilot.Reflexes + 100 - navigator.Communication) / 20;
                    var chanceForSalvation = rnd.Next(1, randomNumberForSalvation);
                    if (chanceForSalvation == 1 || chanceForSalvation == 2 || chanceForSalvation == 3)
                    {
                        return "1";
                    }
                    else if (chanceForSalvation == 4 || chanceForSalvation == 5 || chanceForSalvation == 6)
                    {
                        return "10";
                    }
                    else
                    {
                        return "the end";
                    }
                }
            }
            return "OK";
        }
    }
}

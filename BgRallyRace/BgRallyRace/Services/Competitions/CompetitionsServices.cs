﻿namespace BgRallyRace.Services.Competitions
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Services.Runways;
    using BgRallyRace.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using BgRallyRace.Models.Competitions;
    using System.Timers;

    public class CompetitionsServices : ICompetitionsServices
    {
        const int pageSize = 5;
        const decimal damageConstant = (decimal)0.5;

        private readonly ApplicationDbContext dbContext;
        private readonly ICarServices cars;
        private readonly ITeamServices team;
        private readonly IRallyPilotsServices pilots;
        private readonly IRallyNavigatorsServices navigators;
        private readonly IRunwaysServices runways;
        private readonly IRaceHistoryServices raceHistory;
        private readonly IPeople people;
        private readonly IMoneyAccountServices money;
        private readonly IRatingListServices ratingList;

        public CompetitionsServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public CompetitionsServices(ApplicationDbContext dbContext, ICarServices carServices, ITeamServices teamServices,
            IRallyPilotsServices pilotsServices, IRallyNavigatorsServices navigatorsServices, IRunwaysServices runwaysServices,
            IRaceHistoryServices raceHistoryServices, IPeople people, IRatingListServices ratingListServices,
            IMoneyAccountServices moneyAccountServices)
        {
            this.dbContext = dbContext;
            this.cars = carServices;
            this.team = teamServices;
            this.pilots = pilotsServices;
            this.navigators = navigatorsServices;
            this.runways = runwaysServices;
            this.raceHistory = raceHistoryServices;
            this.people = people;
            this.money = moneyAccountServices;
            this.ratingList = ratingListServices;
        }

        public async Task AddTime(string team, DateTime time)
        {
            var times = await dbContext.CompetitionsTeam
                .Where(x => x.Team.Name == team && x.Competition.Applicable == true)
                .FirstAsync();
            times.Time = time;
            await dbContext.SaveChangesAsync();
        }

        public async Task<DateTime> GetStartDate()
        {
            var date = await dbContext
                .Competitions
                .Where(x => x.Applicable == true)
                .Select(x => x.StartRaceDate)
                .FirstOrDefaultAsync();
            return date;
        }

        public async Task<int> GetCompetitionId()
        {
            var date = await dbContext
                .Competitions
                .Where(x => x.Applicable == true)
                .Select(x => x.Id)
                .FirstAsync();
            return date;
        }

        public async Task<Competitions> GetCompetition(int id)
        {
            var date = await dbContext
                .Competitions
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return date;
        }

        public async Task<CompetitionsRallyRunway> GetCompetitionRunway(int id)
        {
            var date = await dbContext
                .CompetitionsRallyRunway
                .Where(x => x.CompetitionsId == id)
                .FirstOrDefaultAsync();
            return date;
        }

        public async Task<List<Competitions>> GetAllCompetitions(int page)
        {
            var date = await dbContext
                .Competitions
                .Where(x => x.Applicable == true)
                 .Skip((page - 1) * pageSize)
                .Take(5)
                .ToListAsync();
            return date;
        }

        public int TotalPage()
        {
            var result = dbContext.Competitions
               .Where(x => x.Applicable == true)
              .Count();
            return result;
        }

        public async Task<string> GetCompetitionName()
        {
            var date = await dbContext
                .Competitions
                .Where(x => x.Applicable == true)
                .Select(x => x.Name)
                .FirstAsync();
            return date;
        }

        public async Task<decimal> GetCompetitionPrizeFund()
        {
            var date = await dbContext
                .Competitions
                .Where(x => x.Applicable == true)
                .Select(x => x.PrizeFund)
                .FirstOrDefaultAsync();
            return date;
        }

        public async Task ActivateNextRace()
        {
            var old = dbContext
                .Competitions
                 .Where(x => x.Applicable == true)
                .FirstOrDefault();
            old.Applicable = false;
            var date = dbContext
                .Competitions
                .Where(x => x.StartRaceDate > DateTime.Now)
                .FirstOrDefault();
            date.Applicable = true;
            await dbContext.SaveChangesAsync();
        }

        public void HasIsStartedAsync()
        {

            // System.Timers.Timer aTimer = new System.Timers.Timer();
            // aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            // aTimer.Interval = 1000*60;
            // aTimer.Enabled = true;

            var date = GetStartDate();

            var t = Task.Run(() =>
            {
                while (true)
                {
                    var nowDate = DateTime.Now;
                    if (date.Result.Date == nowDate.Date)
                    {
                        while (true)
                        {
                            nowDate = DateTime.Now;
                            if (date.Result.Hour == nowDate.Hour)
                            {
                                while (true)
                                {
                                    nowDate = DateTime.Now;
                                    if (date.Result.Minute == nowDate.Minute)
                                    {
                                        this.StartRally();
                                    }
                                    Thread.Sleep(1000 * 60);//1 minute
                               }
                            }
                            Thread.Sleep(1000 * 60 * 60);//1 hour
                       }
                    }
                    Thread.Sleep(1000 * 60 * 60 * 24);//1 day
               }
            });

        }

        // private void OnTimedEvent(object source, ElapsedEventArgs e)
        // {
        //     var date = GetStartDate().GetAwaiter().GetResult();
        //     var nowDate = DateTime.Now;
        //     if (date.Date.Minute == nowDate.Date.Minute)
        //     {
        //          this.StartRally();
        //      }
        // }

        public async Task<string> RallyЕntry(TeamViewModels input)
        {
            var team = await dbContext.CompetitionsTeam
                .Where(x => x.TeamId == input.TeamId && x.Competition.Applicable == true)
                .FirstOrDefaultAsync();
            if (team != null)
            {
                return $"Вие вече сте записан за стезанието.";
            }

            await dbContext.CompetitionsTeam.AddAsync(new CompetitionsTeams
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
            return $"Успешно се записахте за състезанието {input.Name}.";
        }

        public void StartRally()
        {
            var raceName = GetCompetitionName().GetAwaiter().GetResult();
            var teams = GetAllTeamsAsync().GetAwaiter().GetResult();
            var startTeams = teams;
            pilots.AllPilotsNoWorking();
            navigators.AllNavigatorNoWorking();
            var runway = runways.GetRunwayForRaceAsync().Result;
           var stageOne = runway.TrackLength / 4;
           var stageTwo = runway.TrackLength / 2;
           var stageThree = runway.TrackLength / 4;
            var data = GetStartDate().GetAwaiter().GetResult();
            List<CompetitionsTeams> crashed = new List<CompetitionsTeams>();

            string input = $"Бе дадено началото на състезанието {raceName}, провежащо се на дата: {data.ToString("D")} " +
                $"на писта {runway.Name}.";//ToDo
            raceHistory.AddHistory(input);

            input = "Очакваме всеки момент да потегли първият автомобил.";
            raceHistory.AddHistory(input);

            var count = teams.Count;
            CompetitiveStage(pilots, people, navigators, team, raceHistory, cars, teams, runway, count, crashed, stageOne, 1);//1 comes from the number of Stage - stageOne

            for (int j = 0; j < crashed.Count; j++)
            {
                teams.Remove(crashed[j]);
            }

            crashed = new List<CompetitionsTeams>();

            input = $"В надпреварата продължават {teams.Count} автомобила. Нека видим как ще се развие състезанието.";
            raceHistory.AddHistory(input);

            count = teams.Count;

            CompetitiveStage(pilots, people, navigators, team, raceHistory, cars, teams, runway, count, crashed, stageTwo, 2);//2 comes from the number of Stage - stageTwo

            for (int j = 0; j < crashed.Count; j++)
            {
                teams.Remove(crashed[j]);
            }

            crashed = new List<CompetitionsTeams>();

            input = $"В последния етап на надпреварата продължават {teams.Count} автомобила. Нека видим как ще се завърши състезанието.";
            raceHistory.AddHistory(input);

            ratingList.AddPontsSE();

            count = teams.Count;

            CompetitiveStage(pilots, people, navigators, team, raceHistory, cars, teams, runway, count, crashed, stageThree, 3);//3 comes from the number of Stage - stageThree

            ratingList.AddPonts();

            input = $"Състезанието {raceName} завърши. Нека погледнем крайните времена.";
            raceHistory.AddHistory(input);

            var dictionary = ratingList.GetRatingList();

            foreach (var item in dictionary)
            {
                input = $"Отбор {item.Key.Name} завършва с време {item.Value.Minute}";
                raceHistory.AddHistory(input);
                AddTime(item.Key.Name, item.Value);
            }

            input = $"До нови срещи приятели! Очаквам ви отново на следващото състезание.";
            raceHistory.AddHistory(input);

            var prizeFund = GetCompetitionPrizeFund().Result;
            var winners = ratingList.DistributionPoint();
            money.DistributionOfPrizeMoney(prizeFund, startTeams, winners);

            raceHistory.CreateHistory(teams[0].CompetitionId, raceName);

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            ActivateNextRace();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private async Task<List<CompetitionsTeams>> GetAllTeamsAsync()
        {
            var teams = await dbContext
                .CompetitionsTeam
                .Where(x => x.Competition.Applicable == true)
                .ToListAsync();
            return teams;
        }

        private string RandomEvents(RallyPilots pilot, RallyNavigators navigator, DifficultyType type, int drive)
        {
            var constantDifficul = 8;
            var constantAverage = 4;
            var constantEasy = 2;
            Random rnd = new Random();
            var chanceOfAnEvent = ((100 - pilot.Concentration) + (100 - navigator.Concentration)) / 4;
            int randomNumberForIncident = 0;
            int randomNumberForSalvation;
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
                    randomNumberForIncident = constantDifficul + constantDifficul * chanceOfAnEvent / 100 - 2;
                }
                var chanceForIncident = rnd.Next(1, randomNumberForIncident);
                if (!(chanceForIncident == 1 || chanceForIncident == 2 || chanceForIncident == 3))
                {
                    randomNumberForSalvation = (100 - pilot.Reflexes + 100 - navigator.Communication) / 20;
                    var chanceForSalvation = rnd.Next(1, randomNumberForSalvation);
                    if (chanceForSalvation == 1 || chanceForSalvation == 2 || chanceForSalvation == 3)
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
            return "Ok";
        }

        private void CompetitiveStage(IRallyPilotsServices pilots, IPeople people, IRallyNavigatorsServices navigators,
    ITeamServices team, IRaceHistoryServices raceHistory, ICarServices cars, List<CompetitionsTeams> teams,
    RallyRunway runway, int count, List<CompetitionsTeams> crashed, decimal stages, int numberStage)
        {

            string input = string.Empty;
            for (int i = 0; i < count; i++)
            {
                var pilot = pilots.GetPilotNoTracking(teams[i].PilotId);
                var energyPilot = pilots.EnergyPilot(teams[i].PilotId) - stages * damageConstant;
                people.ReduceEnergy(pilot, energyPilot);
                var navigator = navigators.GetNavigator(teams[i].NavigatorId);
                var energyNavigator = navigators.EnergyNavigator(teams[i].NavigatorId) - stages * damageConstant;
                people.ReduceEnergy(navigator, energyNavigator);
                var teamRace = team.FindUserAsync(teams[i].TeamId).GetAwaiter().GetResult();
                int drive;

                if (numberStage == 1)
                {
                    if (i == 0)
                    {
                        if (teams[i].Drive == DriveType.Aggressive)
                        {
                            input = $"Първи стартира отборът на {teamRace.Name}, с пилот {pilot.FirstName}, който тръгва с мръсна газ. Наблюдаваме доста агресивно" +
               $" каране дано, навигатор до него {navigator.FirstName} му влее малко разум.";
                            drive = 3;
                        }
                        else if (teams[i].Drive == DriveType.Normal)
                        {
                            input = $"Първи стартира отборът на {teamRace.Name}, с пилот {pilot.FirstName}" +
              $" и навигатор до него {navigator.FirstName} ";
                            drive = 2;
                        }
                        else
                        {
                            input = $"Първи стартира отборът на {teamRace.Name}, с пилот {pilot.FirstName}, който потегля доста плахо. С това предпазливо каране, ще " +
             $"изостане доста в класацията, дано навигатор до него {navigator.FirstName} му повлияе полужително.";
                            drive = 1;
                        }

                    }
                    else
                    {
                        if (teams[i].Drive == DriveType.Aggressive)
                        {
                            input = $"Следващ е отборът на {teamRace.Name}, с пилот {pilot.FirstName}, който тръгва с мръсна газ. Наблюдаваме доста агресивно" +
               $" каране дано, навигатор до него {navigator.FirstName} му влее малко разум.";
                            drive = 3;
                        }
                        else if (teams[i].Drive == DriveType.Normal)
                        {
                            input = $"Следващия отборът на старта е {teamRace.Name}, с пилот {pilot.FirstName}" +
              $" и навигатор до него {navigator.FirstName} ";
                            drive = 2;
                        }
                        else
                        {
                            input = $"Към старт линията се приближава отборът на {teamRace.Name}, с пилот {pilot.FirstName}, който потегля доста плахо. С това предпазливо каране, ще " +
             $"изостане доста в класацията, дано навигатор до него {navigator.FirstName} му повлияе полужително.";
                            drive = 1;
                        }
                    }
                }
                else
                {
                    if (teams[i].Drive == DriveType.Aggressive)
                    {
                        input = $"Пилот {pilot.FirstName} от отборът на {teamRace.Name}, продължава с агресивното шофиране. Не съм обеден че това ще му донесе привилегии " +
$" за напред. Дано, навигатор до него {navigator.FirstName} го успокой за да не станем свидетели на катастрофа.";
                        drive = 3;
                    }
                    else if (teams[i].Drive == DriveType.Normal)
                    {
                        input = $"Пилот {pilot.FirstName} от отборът на {teamRace.Name}, продължава с едно чудестно представяне.";

                        drive = 2;
                    }
                    else
                    {
                        input = $"Пилот {pilot.FirstName} от отборът на {teamRace.Name}, продължава с плахото шофиране. Очаквах повече от този пилот " +
$"Дано навигатор до него {navigator.FirstName} даде малко смелост да натисне педала.";
                        drive = 1;
                    }
                }

                raceHistory.AddHistory(input);

                var random = RandomEvents(pilot, navigator, runway.Difficulty, drive);
                if (random == "the end")
                {
                    input = $"Мик невнимание и пилота {pilot.FirstName} изпусна завоя, колата се преобърна в канавката. Дано всички са наред. " +
                        $"Това беше края на състезанието за отбор: {teamRace.Name}";
                    var date = new DateTime();
                    ratingList.AddInRatingList(teamRace, date);
                    crashed.Add(teams[i]);
                }
                else if (random == "Ok")
                {
                    input = $"Без проблеми отборрът на {teamRace.Name} с {pilot.FirstName} зад волана и навигатор до него {navigator.FirstName}" +
                        $"преминаха през втората контрола";
                    var turbo = cars.GetTurbo(teamRace.User);
                    if (turbo != null && teams[i].UseOfTurboType == UseOfTurboType.Medium)
                    {
                        var speed = (cars.GetMaxCurrentSpeed(teamRace.User) + cars.GetTurbo(teamRace.User).Speed) / 60; //to convene in km / h
                        double time = (double)stages / (double)speed;
                        var date = new DateTime();
                        var realTime = date.AddMinutes(time);
                        ratingList.AddInRatingList(teamRace, realTime);
                    }
                    else
                    {
                        var speed = cars.GetMaxCurrentSpeed(teamRace.User) / 60; //to convene in km / h
                        double time = (double)stages / (double)speed;
                        var date = new DateTime();
                        var realTime = date.AddMinutes(time);
                        ratingList.AddInRatingList(teamRace, realTime);
                    }

                }
                else
                {
                    input = $"Пилота {pilot.FirstName} от отбора на {teamRace.Name}, се разсея за момент и излезе от идеалната линия. Това му костваше цени секунди";
                    var turbo = cars.GetTurbo(teamRace.User);
                    if (turbo != null && teams[i].UseOfTurboType == UseOfTurboType.Medium)
                    {
                        var speed = (cars.GetMaxSpeed(teamRace.User) + cars.GetTurbo(teamRace.User).Speed) / 60; //to convene in km / h
                        double time = (double)stages / (double)speed;
                        var date = new DateTime();
                        var realTime = date.AddMinutes(time).AddSeconds(double.Parse(random));
                        ratingList.AddInRatingList(teamRace, realTime);
                    }
                    else
                    {
                        var speed = cars.GetMaxSpeed(teamRace.User) / 60; //to convene in km / h
                        double time = (double)stages / (double)speed;
                        var date = new DateTime();
                        var realTime = date.AddMinutes(time).AddSeconds(double.Parse(random));
                        ratingList.AddInRatingList(teamRace, realTime);
                    }
                }

                raceHistory.AddHistory(input);

                pilots.DecreaseEnergy(pilot.Id, 100 - (int)energyPilot);
                pilots.IncreaseExperience(pilot.Id, numberStage);//2 comes from the number of Stage - stageTwo
                navigators.DecreaseEnergy(navigator.Id, 100 - (int)energyNavigator);
                navigators.IncreaseExperience(navigator.Id, numberStage);//2 comes from the number of Stage - stageTwo
                cars.Damage(teamRace.CarId, numberStage, runway.Difficulty);//2 comes from the number of Stage - stageTwo
            }
        }
    }
}

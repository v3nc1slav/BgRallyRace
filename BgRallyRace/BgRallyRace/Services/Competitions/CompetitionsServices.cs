namespace BgRallyRace.Services.Competitions
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Services.Runways;
    using BgRallyRace.ViewModels;
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
        private readonly IMoneyAccountServices money;
        private readonly IRatingListServices ratingList;

        const decimal damageConstant = (decimal)0.5;

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

        public void AddTime(string team, DateTime time)
        {
           var times = dbContext.CompetitionsTeam.Where(x => x.Team.Name == team).Select(x => x.Time).FirstOrDefault();
           times = time;
            dbContext.SaveChanges();
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

        public decimal GetCompetitionPrizeFund()
        {
            var date = dbContext
                .Competitions
                .Where(x => x.Applicable == true)
                .Select(x => x.PrizeFund)
                .FirstOrDefault();
            return date;
        }

        public void ActivateNextRace()
        {
            var date = dbContext
                .Competitions
                .Where(x => x.StartRaceDate > DateTime.Now)
                .Select(x => x.Applicable)
                .FirstOrDefault();
            date = true;
            dbContext.SaveChanges();
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
                    Thread.Sleep(1000 * 60 * 5);
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
            var teams = GetAllTeamsAsync().ToList();
            pilots.AllPilotsNoWorking();
            navigators.AllNavigatorNoWorking();
            var runway = runways.GetRunwayForCurrentRace();
            var stageOne = runway.TrackLength / 4;
            var stageTwo = runway.TrackLength / 2;
            var stageThree = runway.TrackLength / 4;
            var data = GetStartDate();

            string input = $"Бе дадено началото на състезанието {raceName}, провежащо се на дата: {data} " +
                $"на писта {runway.Name}.";//ToDo
            raceHistory.AddHistory(input);

            input = "Очакваме всеки момент да потегли първият автомобил.";
            raceHistory.AddHistory(input);

            var count = teams.Count;
            for (int i = 0; i < count; i++)
            {
                var pilot = pilots.GetPilotNoTracking(teams[i].PilotId);
                var energyPilot = pilots.EnergyPilot(teams[i].PilotId) - stageOne * damageConstant;
                people.ReduceEnergy(pilot, energyPilot);
                var navigator = navigators.GetNavigator(teams[i].NavigatorId);
                var energyNavigator = navigators.EnergyNavigator(teams[i].NavigatorId) - stageOne * damageConstant;
                people.ReduceEnergy(navigator, energyNavigator);
                var teamRace = team.FindUser(teams[i].TeamId);
                int drive = 0;
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

                raceHistory.AddHistory(input);

                var random = RandomEvents(pilot, navigator, runway.Difficulty, drive);
                if (random == "the end")
                {
                    input = $"Мик невнимание и пилота {pilot.FirstName} изпусна завоя, колата се преобърна в канавката. Дано всички са наред. " +
                        $"Това беше края на състезанието за отбор: {teamRace.Name}";
                    var date = new DateTime();
                    ratingList.AddInRatingList(teamRace, date);
                    teams.RemoveAt(i);
                }
                else if (random == "Ok")
                {
                    input = $"Без проблеми отборрът на {teamRace.Name} с {pilot.FirstName} зад волана и навигатор до него {navigator.FirstName}" +
                        $"преминаха през първата контрола";
                    var turbo = cars.GetTurbo(teamRace.User);
                    if (turbo == null)
                    {
                        var speed = cars.GetMaxSpeed(teamRace.User) / 60; //to convene in km / h
                        double time = (double)stageOne / (double)speed;
                        var date = new DateTime();
                        var realTime = date.AddMinutes(time);
                        ratingList.AddInRatingList(teamRace, realTime);
                    }
                    else if (teams[i].UseOfTurboType == UseOfTurboType.Start)
                    {
                        var speed = (cars.GetMaxSpeed(teamRace.User) + cars.GetTurbo(teamRace.User).Speed) / 60; //to convene in km / h
                        double time = (double)stageOne / (double)speed;
                        var date = new DateTime();
                        var realTime = date.AddMinutes(time);
                        ratingList.AddInRatingList(teamRace, realTime);
                    }
                }
                else
                {
                    input = $"Пилота {pilot.FirstName} от отбора на {teamRace.Name}, се разсея за момент и излезе от идеалната линия. Това му костваше цени секунди";
                    var turbo = cars.GetTurbo(teamRace.User);
                    if (turbo == null)
                    {
                        var speed = cars.GetMaxSpeed(teamRace.User) / 60; //to convene in km / h
                        double time = (double)stageOne / (double)speed;
                        var date = new DateTime();
                        var realTime = date.AddMinutes(time).AddSeconds(double.Parse(random));
                        ratingList.AddInRatingList(teamRace, realTime);
                    }
                    else if (teams[i].UseOfTurboType == UseOfTurboType.Start)
                    {
                        var speed = (cars.GetMaxSpeed(teamRace.User) + cars.GetTurbo(teamRace.User).Speed) / 60; //to convene in km / h
                        double time = (double)stageOne / (double)speed;
                        var date = new DateTime();
                        var realTime = date.AddMinutes(time).AddSeconds(double.Parse(random));
                        ratingList.AddInRatingList(teamRace, realTime);
                    }
                }

                raceHistory.AddHistory(input);

                pilots.DecreaseEnergy(pilot.Id,100- (int)energyPilot);
                pilots.IncreaseExperience(pilot.Id, 1);//1 comes from the number of Stage - StageOne
                navigators.DecreaseEnergy(navigator.Id,100- (int)energyNavigator);
                navigators.IncreaseExperience(navigator.Id, 1);//1 comes from the number of Stage - StageOne
                cars.Damage(teamRace.CarId, 1, runway.Difficulty);//1 comes from the number of Stage - StageOne
            }

            input = $"В надпреварата продължават {teams.Count} автомобила. Нека видим как ще се развие състезанието.";
            raceHistory.AddHistory(input);

            count = teams.Count;
            for (int i = 0; i < count; i++)
            {
                var pilot = pilots.GetPilotNoTracking(teams[i].PilotId);
                var energyPilot = pilots.EnergyPilot(teams[i].PilotId) - stageTwo * damageConstant;
                people.ReduceEnergy(pilot, energyPilot);
                var navigator = navigators.GetNavigator(teams[i].NavigatorId);
                var energyNavigator = navigators.EnergyNavigator(teams[i].NavigatorId) - stageTwo * damageConstant;
                people.ReduceEnergy(navigator, energyNavigator);
                var teamRace = team.FindUser(teams[i].TeamId);
                int drive = 0;
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

                raceHistory.AddHistory(input);

                var random = RandomEvents(pilot, navigator, runway.Difficulty, drive);
                if (random == "the end")
                {
                    input = $"Мик невнимание и пилота {pilot.FirstName} изпусна завоя, колата се преобърна в канавката. Дано всички са наред. " +
                        $"Това беше края на състезанието за отбор: {teamRace.Name}";
                    var date = new DateTime();
                    ratingList.AddInRatingList(teamRace, date);
                    teams.RemoveAt(i);
                }
                else if (random == "Ok")
                {
                    input = $"Без проблеми отборрът на {teamRace.Name} с {pilot.FirstName} зад волана и навигатор до него {navigator.FirstName}" +
                        $"преминаха през втората контрола";
                    var turbo = cars.GetTurbo(teamRace.User);
                    if (turbo == null)
                    {
                        var speed = cars.GetMaxSpeed(teamRace.User) / 60; //to convene in km / h
                        double time = (double)stageTwo / (double)speed;
                        var date = new DateTime();
                        var realTime = date.AddMinutes(time);
                        ratingList.AddInRatingList(teamRace, realTime);
                    }
                    else if (teams[i].UseOfTurboType == UseOfTurboType.Medium)
                    {
                        var speed = (cars.GetMaxSpeed(teamRace.User) + cars.GetTurbo(teamRace.User).Speed) / 60; //to convene in km / h
                        double time = (double)stageTwo / (double)speed;
                        var date = new DateTime();
                        var realTime = date.AddMinutes(time);
                        ratingList.AddInRatingList(teamRace, realTime);
                    }
                }
                else
                {
                    input = $"Пилота {pilot.FirstName} от отбора на {teamRace.Name}, се разсея за момент и излезе от идеалната линия. Това му костваше цени секунди";
                    var turbo = cars.GetTurbo(teamRace.User);
                    if (turbo == null)
                    {
                        var speed = cars.GetMaxSpeed(teamRace.User) / 60; //to convene in km / h
                        double time = (double)stageTwo / (double)speed;
                        var date = new DateTime();
                        var realTime = date.AddMinutes(time).AddSeconds(double.Parse(random));
                        ratingList.AddInRatingList(teamRace, realTime);
                    }
                    else if (teams[i].UseOfTurboType == UseOfTurboType.Start)
                    {
                        var speed = (cars.GetMaxSpeed(teamRace.User) + cars.GetTurbo(teamRace.User).Speed) / 60; //to convene in km / h
                        double time = (double)stageTwo / (double)speed;
                        var date = new DateTime();
                        var realTime = date.AddMinutes(time).AddSeconds(double.Parse(random));
                        ratingList.AddInRatingList(teamRace, realTime);
                    }
                }

                raceHistory.AddHistory(input);

                pilots.DecreaseEnergy(pilot.Id,100- (int)energyPilot);
                pilots.IncreaseExperience(pilot.Id, 2);//2 comes from the number of Stage - stageTwo
                navigators.DecreaseEnergy(navigator.Id,100- (int)energyNavigator);
                navigators.IncreaseExperience(navigator.Id, 2);//2 comes from the number of Stage - stageTwo
                cars.Damage(teamRace.CarId, 2, runway.Difficulty);//2 comes from the number of Stage - stageTwo
            }

            input = $"В последния етап на надпреварата продължават {teams.Count} автомобила. Нека видим как ще се завърши състезанието.";
            raceHistory.AddHistory(input);

            ratingList.AddPontsSE();

            count = teams.Count;
            for (int i = 0; i < count; i++)
            {
                var pilot = pilots.GetPilotNoTracking(teams[i].PilotId);
                var energyPilot = pilots.EnergyPilot(teams[i].PilotId) - stageTwo * damageConstant;
                people.ReduceEnergy(pilot, energyPilot);
                var navigator = navigators.GetNavigator(teams[i].NavigatorId);
                var energyNavigator = navigators.EnergyNavigator(teams[i].NavigatorId) - stageTwo * damageConstant;
                people.ReduceEnergy(navigator, energyNavigator);
                var teamRace = team.FindUser(teams[i].TeamId);
                int drive = 0;
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

                raceHistory.AddHistory(input);

                var random = RandomEvents(pilot, navigator, runway.Difficulty, drive);
                if (random == "the end")
                {
                    input = $"Мик невнимание и пилота {pilot.FirstName} изпусна завоя, колата се преобърна в канавката. Дано всички са наред. " +
                        $"Това беше края на състезанието за отбор: {teamRace.Name}";
                    var date = new DateTime();
                    ratingList.AddInRatingList(teamRace, date);
                }
                else if (random == "Ok")
                {
                    input = $"Без проблеми отборрът на {teamRace.Name} с {pilot.FirstName} зад волана и навигатор до него {navigator.FirstName}" +
                        $"преминаха през третия етап";
                    var turbo = cars.GetTurbo(teamRace.User);
                    if (turbo == null)
                    {
                        var speed = cars.GetMaxSpeed(teamRace.User) / 60; //to convene in km / h
                        double time = (double)stageThree / (double)speed;
                        var date = new DateTime();
                        var realTime = date.AddMinutes(time);
                        ratingList.AddInRatingList(teamRace, realTime);
                    }
                    else if (teams[i].UseOfTurboType == UseOfTurboType.Edge)
                    {
                        var speed = (cars.GetMaxSpeed(teamRace.User) + cars.GetTurbo(teamRace.User).Speed) / 60; //to convene in km / h
                        double time = (double)stageThree / (double)speed;
                        var date = new DateTime();
                        var realTime = date.AddMinutes(time);
                        ratingList.AddInRatingList(teamRace, realTime);
                    }
                }
                else
                {
                    input = $"Пилота {pilot.FirstName} от отбора на {teamRace.Name}, се разсея за момент и излезе от идеалната линия. Това му костваше цени секунди";
                    var turbo = cars.GetTurbo(teamRace.User);
                    if (turbo == null)
                    {
                        var speed = cars.GetMaxSpeed(teamRace.User) / 60; //to convene in km / h
                        double time = (double)stageThree / (double)speed;
                        var date = new DateTime();
                        var realTime = date.AddMinutes(time).AddSeconds(double.Parse(random));
                        ratingList.AddInRatingList(teamRace, realTime);
                    }
                    else if (teams[i].UseOfTurboType == UseOfTurboType.Start)
                    {
                        var speed = (cars.GetMaxSpeed(teamRace.User) + cars.GetTurbo(teamRace.User).Speed) / 60; //to convene in km / h
                        double time = (double)stageThree / (double)speed;
                        var date = new DateTime();
                        var realTime = date.AddMinutes(time).AddSeconds(double.Parse(random));
                        ratingList.AddInRatingList(teamRace, realTime);
                    }
                }

                raceHistory.AddHistory(input);

                pilots.DecreaseEnergy(pilot.Id,100- (int)energyPilot);
                pilots.IncreaseExperience(pilot.Id, 3);//2 comes from the number of Stage - stageThree
                navigators.DecreaseEnergy(navigator.Id,100- (int)energyNavigator);
                navigators.IncreaseExperience(navigator.Id, 3);//2 comes from the number of Stage - stageThree
                cars.Damage(teamRace.CarId, 3, runway.Difficulty);//2 comes from the number of Stage - stageThree
            }

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

            var prizeFund = GetCompetitionPrizeFund();
            money.DistributionOfPrizeMoney(prizeFund, teams);

            raceHistory.CreateHistory(teams[0].CompetitionId, raceName);

            ActivateNextRace();
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
            var chanceOfAnEvent = ((100 - pilot.Concentration) + (100 - navigator.Concentration)) / 4;
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
    }
}

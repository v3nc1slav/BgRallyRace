namespace BgRallyRace.Services.Competitions
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RatingListServices : IRatingListServices
    {
        Dictionary<Team, DateTime> DictionaryTeams = new Dictionary<Team, DateTime>();

        private readonly ApplicationDbContext dbContext;

        public RatingListServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddInRatingList(Team team, DateTime time)
        {
            if (DictionaryTeams.ContainsKey(team))
            {
                DictionaryTeams[team].AddMinutes(time.Minute);
            }
            if (time.Minute == 0)
            {
                DictionaryTeams[team] = new DateTime();
            }
            DictionaryTeams.Add(team, time);
        }

        public void AddPonts()
        {
            var teams = DistributionPoint();
            for (int i = 0; i < 10; i++)
            {
                var points = dbContext.CompetitionsTeam.Where(x => x.TeamId == teams[i].Id).Select(x => x.Points).FirstOrDefault();
                switch (i + 1)
                {
                    case 1:
                        points += 25;
                        break;
                    case 2:
                        points += 18;
                        break;
                    case 3:
                        points += 15;
                        break;
                    case 4:
                        points += 12;
                        break;
                    case 5:
                        points += 10;
                        break;
                    case 6:
                        points += 8;
                        break;
                    case 7:
                        points += 6;
                        break;
                    case 8:
                        points += 4;
                        break;
                    case 9:
                        points += 2;
                        break;
                    case 10:
                        points += 1;
                        break;
                    default:
                        points += 0;
                        break;
                }
                dbContext.SaveChanges();
            }
        }

        public Dictionary<Team, DateTime> GetRatingList()
        {
            return DictionaryTeams;
        }

        private List<Team> DistributionPoint()
        {
            var teams = new List<Team>();
            var dictionary = GetRatingList();
            var count = dictionary.Count;
            for (int i = 0; i < count; i++)
            {
                var min = int.MaxValue;
                var team = new Team();
                foreach (var item in dictionary)
                {
                    if (item.Value.Minute != 0 && item.Value.Minute < min)
                    {
                        min = item.Value.Minute;
                        team = item.Key;
                    }
                }
                teams.Add(team);
                dictionary.Remove(team);
            }
            return teams;
        }

        public void AddPontsSE()
        {
            var teams = DistributionPoint();
            for (int i = 0; i < 7; i++)
            {
                var points = dbContext.CompetitionsTeam.Where(x => x.TeamId == teams[i].Id).Select(x => x.Points).FirstOrDefault();
                switch (i + 1)
                {
                    case 1:
                        points += 7;
                        break;
                    case 2:
                        points += 6;
                        break;
                    case 3:
                        points += 5;
                        break;
                    case 4:
                        points += 4;
                        break;
                    case 5:
                        points += 3;
                        break;
                    case 6:
                        points += 2;
                        break;
                    case 7:
                        points += 1;
                        break;
                    default:
                        points += 0;
                        break;
                }
                dbContext.SaveChanges();
            }
        }
    }
}

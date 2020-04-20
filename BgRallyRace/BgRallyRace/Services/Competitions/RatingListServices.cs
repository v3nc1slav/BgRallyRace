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
            else
            {
                DictionaryTeams.Add(team, time);
            }
            if (time.Minute == 0)
            {
                DictionaryTeams[team] = new DateTime();
            }
         
        }

        public void AddPonts()
        {
            var teams = DistributionPoint();
            var count = teams.Count ;
            if (teams.Count  > 10)
            {
                count = 10;
            }
            for (int i = 0; i < count; i++)
            {
                var points = dbContext.CompetitionsTeam.Where(x => x.TeamId == teams[i].Id).FirstOrDefault();
                points.Points = (i + 1) switch
                {
                    1 => points.Points + 25,
                    2 => points.Points + 18,
                    3 => points.Points + 15,
                    4 => points.Points + 12,
                    5 => points.Points + 10,
                    6 => points.Points + 8,
                    7 => points.Points + 6,
                    8 => points.Points + 4,
                    9 => points.Points + 2,
                    10 => points.Points + 1,
                    _ => points.Points + 0,
                };
                dbContext.SaveChanges();
            }
        }

        public Dictionary<Team, DateTime> GetRatingList()
        {
            return DictionaryTeams;
        }

        public List<Team> DistributionPoint()
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
            var count = teams.Count;
            if (teams.Count >6)
            {
                count = 6;
            }
            for (int i = 0; i < count; i++)
            {
                var points = dbContext.CompetitionsTeam.Where(x => x.TeamId == teams[i].Id).FirstOrDefault();
                points.Points = (i + 1) switch
                {
                    1 => points.Points + 7,
                    2 => points.Points + 6,
                    3 => points.Points + 5,
                    4 => points.Points + 4,
                    5 => points.Points + 3,
                    6 => points.Points + 2,
                    7 => points.Points + 1,
                    _ => points.Points + 0,
                };
                dbContext.SaveChanges();
            }
        }
    }
}

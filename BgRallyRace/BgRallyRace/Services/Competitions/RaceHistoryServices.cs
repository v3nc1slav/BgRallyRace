namespace BgRallyRace.Services.Competitions
{
    using BgRallyRace.Data;
    using BgRallyRace.Models.Competitions;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RaceHistoryServices : IRaceHistoryServices
    {
        private readonly ApplicationDbContext dbContext;
        private string history;

        public RaceHistoryServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateHistory(int id, string name)
        {
            dbContext.RaceHistories.Add(new RaceHistory
            {
                CompetitionsId = id,
                CompetitionsName = name,
                History = history,
            });
             dbContext.SaveChanges();
        }

        public void AddHistory(string input)
        {
            history = history + input + "§";
        }

        public List<string> GetHistory()
        {
            var history =  dbContext.RaceHistories.OrderByDescending(x=>x.Id).Select(x => x.History).FirstOrDefault();
            var list  =  history.Split('§').ToList();
            return list;
        }
    }
}

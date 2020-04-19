using BgRallyRace.Data;
using BgRallyRace.Models.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services.Competitions
{
    public class RaceHistoryServices : IRaceHistoryServices
    {
        private readonly ApplicationDbContext dbContext;
        string histrory;

        public RaceHistoryServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateHistory(int id, string name, string history)
        {
            dbContext.RaceHistories.Add(new RaceHistory
            {
                CompetitionsId = id,
                CompetitionsName = name,
                History = history,
            });
            await dbContext.SaveChangesAsync();
        }

        public void AddHistory(string input)
        {
            histrory = histrory + input + "§";
        }

    }
}

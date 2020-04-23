namespace BgRallyRace.Services.Competitions
{
    using BgRallyRace.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BgRallyRace.Models.Competitions;


    public interface ICompetitionsServices
    {
        Task<DateTime> GetStartDate();

        void AddTime(string team, DateTime time);

        public int GetCompetitionId();

        public void HasIsStartedAsync();

        public string RallyЕntry(TeamViewModels input);

       public  Task <string> GetCompetitionName();

        void StartRally();

        Task<CompetitionsRallyRunway> GetCompetitionRunway(int id);

        Task<Competitions> GetCompetition(int id);

        Task<List<Competitions>> GetAllCompetitions(int page);

        int TotalPage();
    }
}

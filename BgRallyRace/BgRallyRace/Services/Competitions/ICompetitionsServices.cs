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

        Task AddTime(string team, DateTime time);

        Task<int> GetCompetitionId();

        void HasIsStartedAsync();

        Task<string> RallyЕntry(TeamViewModels input);

       Task <string> GetCompetitionName();

        void StartRally();

        Task<CompetitionsRallyRunway> GetCompetitionRunway(int id);

        Task<Competitions> GetCompetition(int id);

        Task<List<Competitions>> GetAllCompetitions(int page);

        int TotalPage();

        Task<decimal> GetCompetitionPrizeFund();
    }
}

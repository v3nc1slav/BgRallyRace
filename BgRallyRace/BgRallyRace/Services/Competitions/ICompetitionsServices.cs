namespace BgRallyRace.Services.Competitions
{
    using BgRallyRace.ViewModels;
    using System;
    using System.Threading.Tasks;


    public interface ICompetitionsServices
    {
        public  Task<DateTime> GetStartDate();

        public Task<int> GetCompetitionId();

        public Task HasIsStartedAsync();

        public Task RallyЕntry(TeamViewModels input);

        Task<string> GetCompetitionName();

        void StartRalli();
    }
}

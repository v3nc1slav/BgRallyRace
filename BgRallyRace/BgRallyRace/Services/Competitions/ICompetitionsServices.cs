namespace BgRallyRace.Services.Competitions
{
    using BgRallyRace.ViewModels;
    using System;
    using System.Threading.Tasks;


    public interface ICompetitionsServices
    {
        Task<DateTime> GetStartDate();

        void AddTime(string team, DateTime time);

        public int GetCompetitionId();

        public void HasIsStartedAsync();

        public string RallyЕntry(TeamViewModels input);

       public  Task <string> GetCompetitionName();

        void StartRally();
    }
}

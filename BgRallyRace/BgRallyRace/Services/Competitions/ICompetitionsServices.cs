namespace BgRallyRace.Services.Competitions
{
    using BgRallyRace.ViewModels;
    using System;
    using System.Threading.Tasks;


    public interface ICompetitionsServices
    {
        public  DateTime GetStartDate();

        void AddTime(string team, DateTime time);

        public int GetCompetitionId();

        public void HasIsStartedAsync();

        public Task RallyЕntry(TeamViewModels input);

       public string GetCompetitionName();

        void StartRalli();
    }
}

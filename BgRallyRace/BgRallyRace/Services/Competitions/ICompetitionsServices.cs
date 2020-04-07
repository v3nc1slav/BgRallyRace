namespace BgRallyRace.Services.Competitions
{
    using System;
    using System.Threading.Tasks;


    public interface ICompetitionsServices
    {
        public DateTime GetStartDate();
        public Task HasIsStartedAsync();
    }
}

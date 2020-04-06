namespace BgRallyRace.Services.Admin
{
    using BgRallyRace.ViewModels;

    public interface ICreateServices
    {
        public void CreateCompetitions(CompetitionsViewModels input);

        void CreateRunway(RunwayViewModels input);

        void CreatePilot(PilotViewModels input);

        void CreateNavigator(NavigatorViewModels input);

        void CreateParts(PartsViewModels input);
    }
}

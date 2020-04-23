namespace BgRallyRace.Services.Admin
{
    using BgRallyRace.ViewModels;

    public interface ICreateServices
    {
        string CreateCompetitions(CompetitionsViewModels input);

        string CreateRunway(RunwayViewModels input);

        string CreatePilot(PilotViewModels input);

        string CreateNavigator(NavigatorViewModels input);

        string CreateParts(PartsViewModels input);
    }
}

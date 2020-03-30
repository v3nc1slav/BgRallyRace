namespace BgRallyRace.Services.Admin
{
    using BgRallyRace.ViewModels;

    public interface ICreateServices
    {
        void CreateRunway(RunwayViewModels input);

        void CreatePilot(PilotViewModels input);

        void CreateNavigator(NavigatorViewModels input);

        void CreateParts(PartsViewModels input);
    }
}

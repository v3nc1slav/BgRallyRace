namespace BgRallyRace.Services.Admin
{
    using BgRallyRace.ViewModels;
    using System.Threading.Tasks;

    public interface ICreateServices
    {
        Task<string> CreateCompetitionsAsync(CompetitionsViewModels input);

        Task<string> CreateRunwayAsync(RunwayViewModels input);

        Task<string> CreatePilotAsync(PilotViewModels input);

        Task<string> CreateNavigatorAsync(NavigatorViewModels input);

        Task<string> CreateParts(PartsViewModels input);
    }
}

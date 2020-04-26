namespace BgRallyRace.Services
{
    using BgRallyRace.Models;
    using System.Threading.Tasks;

    public interface ITeamServices
    {
        Task CreateTeamAsync(string text, string user);

        Task<Team> FindUserAsync(string user);

        Task<Team> FindUserAsync(int id);

        Task<int> GetTeamIdAsync(string user);
    }
}

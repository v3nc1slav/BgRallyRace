namespace BgRallyRace.Services
{
    using BgRallyRace.Models;
    using System.Threading.Tasks;

    public interface ITeamServices
    {
        void CreateTeam(string text, string user);

        Team FindUser(string user);

        Team FindUser(int id);

        int GetTeamId(string user);
    }
}

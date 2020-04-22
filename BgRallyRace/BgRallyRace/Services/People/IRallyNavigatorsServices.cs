namespace BgRallyRace.Services
{
    using BgRallyRace.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRallyNavigatorsServices : IPeople
    {
        RallyNavigators? GetNavigator(int id);

       void IncreaseCommunication(int id, int variable);

       void DecreaseCommunication(int id, int variable);

        List<RallyNavigators> GetNavigators(string user);

        int CreateRallyNavigatorsAsync();

        Task AllNavigatorNoWorking();

        int EnergyNavigator(int id);

        RallyNavigators GetNavigatorNoTracking(int id);

        List<RallyNavigators> GetPeople();
    }
}

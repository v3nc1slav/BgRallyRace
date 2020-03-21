namespace BgRallyRace.Services
{
    using BgRallyRace.Models;
    using System.Collections.Generic;

    public interface IRallyNavigatorsServices : IPeople
    {
        RallyNavigators? GetNavigator(int id);

       void IncreaseCommunication(int id, int variable);

       void DecreaseCommunication(int id, int variable);

        List<RallyNavigators> GetNavigators(string user);
        int CreateRallyNavigatorsAsync();
    }
}

namespace BgRallyRace.Services
{
    using BgRallyRace.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRallyPilotsServices : IPeople
    {
        void IncreaseReflexes(int id, int variable);

        void DecreaseReflexes(int id, int variable);

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        List<RallyPilots> GetPilots(string user);

#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        RallyPilots GetPilot(int id);

        int CreateRallyPilotsAsync();

       Task AllPilotsNoWorking();

        int EnergyPilot(int id);

        RallyPilots GetPilotNoTracking(int id);

        List<RallyPilots> GetPeople(int page = 1);

        int TotalPilots();
    }
}

namespace BgRallyRace.Services.Admin
{
    using BgRallyRace.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IEditServices
    {
        Task<string> EditRunways(RunwayViewModels newRunway);

        Task<string> EditPilot(PilotViewModels newPilot);

        Task<string> EditCompetitions(CompetitionsViewModels newCompetitions);
    }
}

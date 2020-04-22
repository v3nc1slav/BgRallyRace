namespace BgRallyRace.Services.Admin
{
    using BgRallyRace.Data;
    using BgRallyRace.Services.Runways;
    using BgRallyRace.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EditServices : IEditServices
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IRunwaysServices runways;
        private readonly IRallyPilotsServices pilots;

        public EditServices(ApplicationDbContext dbContext, IRunwaysServices runwaysServices, IRallyPilotsServices pilotsServices)
        {
            this.dbContext = dbContext;
            this.runways = runwaysServices;
            this.pilots = pilotsServices;
        }

        public async Task<string> EditRunways(RunwayViewModels newRunway)
        {
            var oldRunway = runways.GetRunway(newRunway.Id).GetAwaiter().GetResult();
            oldRunway.Name = newRunway.NameRunway;
            oldRunway.Description = newRunway.Description;
            oldRunway.Difficulty = newRunway.Difficulty;
            oldRunway.ImagName = newRunway.ImagName;
            oldRunway.TrackLength = newRunway.TrackLength;
            await dbContext.SaveChangesAsync();
            return "Пистата, е променена успесно.";
        }

        public async Task<string> EditPilot(PilotViewModels newPilot)
        {
            var oldPilot = pilots.GetPilot(newPilot.Id);
            oldPilot.FirstName = newPilot.FirstName;
            oldPilot.LastName = newPilot.LastName;
            oldPilot.Age = newPilot.Age;
            oldPilot.Salary = newPilot.Salary;
            oldPilot.Concentration = newPilot.Concentration;
            oldPilot.Devotion = newPilot.Devotion;
            oldPilot.Reflexes = newPilot.Reflexes;
            oldPilot.Energy = newPilot.Energy;
            oldPilot.Experience = newPilot.Experience;
            oldPilot.PhysicalTraining = newPilot.PhysicalTraining;
            oldPilot.Pounds = newPilot.Pounds;
            await dbContext.SaveChangesAsync();
            return "Пилота, е променен успесно.";
        }

    }
}

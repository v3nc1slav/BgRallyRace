namespace BgRallyRace.Services.Admin
{
    using BgRallyRace.Data;
    using BgRallyRace.Services.Competitions;
    using BgRallyRace.Services.Runways;
    using BgRallyRace.ViewModels;
    using System.Threading.Tasks;

    public class EditServices : IEditServices
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IRunwaysServices runways;
        private readonly IRallyPilotsServices pilots;
        private readonly ICompetitionsServices competitions;

        public EditServices(ApplicationDbContext dbContext, IRunwaysServices runwaysServices, IRallyPilotsServices pilotsServices,
            ICompetitionsServices competitionsServices)
        {
            this.dbContext = dbContext;
            this.runways = runwaysServices;
            this.pilots = pilotsServices;
            this.competitions = competitionsServices;
        }

        public async Task<string> EditRunways(RunwayViewModels newRunway)
        {
            var oldRunway = runways.GetRunwayAsync(newRunway.Id).GetAwaiter().GetResult();
            oldRunway.Name = newRunway.NameRunway;
            oldRunway.Description = newRunway.Description;
            oldRunway.Difficulty = newRunway.Difficulty;
            oldRunway.ImagName = newRunway.ImagName;
            oldRunway.TrackLength = newRunway.TrackLength;
            await dbContext.SaveChangesAsync();
            return "Пистата, е променена успешно.";
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
            return "Пилота, е променен успешно.";
        }

        public async Task<string> EditCompetitions(CompetitionsViewModels newCompetitions)
        {
            var oldCompetitions = await competitions.GetCompetition(newCompetitions.Id);
            oldCompetitions.Name = newCompetitions.Name;
            oldCompetitions.PrizeFund = newCompetitions.PrizeFund;
            oldCompetitions.Stages = newCompetitions.Stages;
            oldCompetitions.StartRaceDate = newCompetitions.StartRaceDate;
            var oldCompetitionsRunway = await competitions.GetCompetitionRunway(newCompetitions.Id);
            for (int i = 0; i < newCompetitions.CompetitionsRallyRunwayId.Count; i++)
            {
                oldCompetitionsRunway.RallyRunwayId = newCompetitions.CompetitionsRallyRunwayId[i];
            }
            await dbContext.SaveChangesAsync();
            return "Състезанието, е променено успешно.";
        }
    }
}

namespace BgRallyRace.Services.Admin
{
    using BgRallyRace.Data;
    using BgRallyRace.Services.Runways;
    using System.Threading.Tasks;

    public class DeleteServices : IDeleteServices
    {
          
        private readonly ApplicationDbContext dbContext;
        private readonly IRunwaysServices runways;
        private readonly IRallyPilotsServices pilots;

        public DeleteServices(ApplicationDbContext dbContext, IRunwaysServices runwaysServices,
            IRallyPilotsServices pilotsServices)
        {
            this.dbContext = dbContext;
            this.runways = runwaysServices;
            this.pilots = pilotsServices;
        }

        public async Task<string> DeleteRunways(int id)
        {
            var runway = runways.GetRunwayAsync(id).GetAwaiter().GetResult();
            runway.IsDeleted = true;
            await dbContext.SaveChangesAsync();
            return "Пистата, е изтрита успешно.";
        }

        public async Task<string> DeletePilots(int id)
        {
            var runway = pilots.GetPilot(id);
            runway.IsDeleted = true;
            await dbContext.SaveChangesAsync();
            return "Пилота, е изтрит успешно.";
        }
    }
}

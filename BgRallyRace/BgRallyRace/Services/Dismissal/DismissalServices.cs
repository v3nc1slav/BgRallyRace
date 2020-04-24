namespace BgRallyRace.Services.Dismissal
{
    using BgRallyRace.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public class DismissalServices : IDismissalServices
    {
        private readonly ApplicationDbContext dbContext;

        public DismissalServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void DismissalPilot(int id)
        {
            var pilot = dbContext.RallyPilots.Where(x => x.Id == id).FirstOrDefault();
            var team = dbContext.Teams.Where(x => x.RallyPilotId == id).FirstOrDefault();
            pilot.TeamId = null;
            team.RallyPilotId = null;
            dbContext.SaveChanges();
        }

        public void DismissalNavigator(int id)
        {
            var navigator = dbContext.RallyNavigators.Where(x => x.Id == id).FirstOrDefault();
            navigator.TeamId = null;
            dbContext.SaveChanges();
        }

        public void DismissalFitter(int id)
        {
            var fitter = dbContext.RallyFitters.Where(x => x.Id == id).FirstOrDefault();
            fitter.TeamId = null;
            dbContext.SaveChanges();
        }
    }
}

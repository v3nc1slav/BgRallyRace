namespace BgRallyRace.Services.Others
{
    using BgRallyRace.Data;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Models.Home;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FAQSarvices : IFAQSarvices
    {
        private readonly ApplicationDbContext dbContext;

        public FAQSarvices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<FAQ>> GetFAQAsync()
        {
            var FAQs = await dbContext.FAQs
                .Where(x=>x.IsDeleted==false 
                && x.authorizationOpinions == AuthorizationType.yes)
                .ToListAsync();
            return FAQs;
        }

    }
}

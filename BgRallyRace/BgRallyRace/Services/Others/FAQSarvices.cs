namespace BgRallyRace.Services.Others
{
    using BgRallyRace.Data;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Models.Home;
    using System;
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

        public List<FAQ> GetFAQ()
        {
            var FAQs =  dbContext.FAQs
                .Where(x=>x.IsDeleted==false && x.authorizationOpinions == AuthorizationType.yes)
                .ToList();
            return FAQs;
        }

    }
}

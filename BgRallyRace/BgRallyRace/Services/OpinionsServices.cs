using BgRallyRace.Data;
using BgRallyRace.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
    public class OpinionsServices
    {
        private readonly ApplicationDbContext dbContext;

        public OpinionsServices()
        {
            
        }
        public OpinionsServices(ApplicationDbContext dbContext)
        {
           
            this.dbContext = dbContext;
        }
        public async Task AddOpinionAsync(string text, string user)
        {
            await dbContext.Opinions.AddAsync(new Opinions { Content= text, 
                DateOfPublication = DateTime.UtcNow, User = user } );
            await dbContext.SaveChangesAsync();
        }

        public string[] GetOpinions()
        {
            var result = dbContext.Opinions.Select(X=>X.Content).ToArray();
            return result;
        }
    }
}

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
        public void  AddOpinion(string text, IdentityUser user)
        {
            dbContext.Opinions.Add(new Opinions { Content= text, DateOfPublication = DateTime.UtcNow, User =  user });
            dbContext.SaveChanges();
        }

        public string[] GetOpinions()
        {
            var result = dbContext.Opinions.Select(x=>x.Content).ToArray();
            return result;
        }
    }
}

namespace BgRallyRace.Services
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using System;
    using System.Linq;
    using BgRallyRace.Models.Enums;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class OpinionsServices : IOpinionsServices
    {
        private readonly ApplicationDbContext dbContext;
        private readonly AuthorizationType indefinitely;
        private readonly AuthorizationType yes;
        private readonly AuthorizationType no;

        public OpinionsServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public OpinionsServices(AuthorizationType yes, AuthorizationType no, AuthorizationType indefinitely)
        {
            this.yes = yes;
            this.no = no;
            this.indefinitely = indefinitely;
        }

        public async Task<string> AddOpinionAsync(string text, string user)
        {
            dbContext.Opinions.Add(new Opinions
            {
                Content = text,
                DateOfPublication = DateTime.UtcNow,
                User = user,
                IsDeleted = false,
                authorizationOpinions = 0
            });
            await dbContext.SaveChangesAsync();
            return "Успешно, публикувахте мнение.";
        }

        public async Task<Opinions[]> GetOpinionsAsync(int page = 1)
        {
            var result = await dbContext.Opinions
                .Where(x => x.authorizationOpinions == AuthorizationType.yes && x.IsDeleted == false)
                .OrderByDescending(x => x.DateOfPublication)
                .Skip((page-1)*10)
                .Take(10) 
                .Select(x => new Opinions 
                {   Id = x.Id, 
                    Content = x.Content, 
                    DateOfPublication = x.DateOfPublication, 
                    User = x.User 
                })
                .ToArrayAsync();
            return result;
        }

        public async Task<Opinions[]> GetOpinionsForAdminAsync(int page = 1)
        {
            var result = await dbContext.Opinions
                .Where(x => x.authorizationOpinions == indefinitely)
                .OrderByDescending(x => x.DateOfPublication)
                .Skip((page - 1) * 10)
                .Take(10)
                .Select(x => new Opinions { Id = x.Id, Content = x.Content })
                .ToArrayAsync();
            return result;
        }

        public async Task MadeOpinionsInvisibleAsync(int[] id)
        {
            for (int i = 0; i < id.Length; i++)
            {
                var option = await dbContext.Opinions.Where(x => x.Id == id[i]).FirstAsync();
                option.authorizationOpinions = no;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task MadeOpinionsVisibleAsync(int[] id)
        {
            for (int i = 0; i < id.Length; i++)
            {
                var option = await dbContext.Opinions.Where(x => x.Id == id[i]).FirstAsync();
                option.authorizationOpinions = yes;
                await dbContext.SaveChangesAsync();
            }
        }

        public int GetCountNotAuthorization()
        {
            var opinions =  this.GetOpinionsForAdminAsync().Result;
            var result = opinions.Length;
            return result;
        }

        public async Task DeleteOpinionAsync(int id)
        {
            var opinions = await dbContext.Opinions
                .Where(x => x.Id == id).FirstAsync();
            opinions.IsDeleted = true;
            await dbContext.SaveChangesAsync();
        }

        public int Total()
        {
           var result = dbContext.Opinions
                .Where(x =>
                x.authorizationOpinions == yes 
                && x.IsDeleted == false)
                .Count();
            return result;
        }
    }
}

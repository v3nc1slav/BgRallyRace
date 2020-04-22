namespace BgRallyRace.Services
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using System;
    using System.Linq;
    using BgRallyRace.Models.Enums;

    public class OpinionsServices : IOpinionsServices
    {
        private readonly ApplicationDbContext dbContext;
        private AuthorizationType indefinitely;
        private AuthorizationType yes;
        private AuthorizationType no;

        public OpinionsServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string AddOpinion(string text, string user)
        {
            dbContext.Opinions.Add(new Opinions
            {
                Content = text,
                DateOfPublication = DateTime.UtcNow,
                User = user,
                IsDeleted = false,
                authorizationOpinions = 0
            });
            dbContext.SaveChanges();
            return "Успешно, публикувахте мнение.";
        }

        public Opinions[] GetOpinions(int page = 1)
        {
            var result = dbContext.Opinions
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
                .ToArray();
            return result;
        }

        public Opinions[] GetOpinionsForAdmin(int page = 1)
        {
            var result = dbContext.Opinions
                .Where(x => x.authorizationOpinions == indefinitely)
                .OrderByDescending(x => x.DateOfPublication)
                .Skip((page - 1) * 10)
                .Take(10)
                .Select(x => new Opinions { Id = x.Id, Content = x.Content }).ToArray();
            return result;
        }

        public void MadeOpinionsInvisible(int[] id)
        {
            for (int i = 0; i < id.Length; i++)
            {
                var option = dbContext.Opinions.Where(x => x.Id == id[i]).First();
                option.authorizationOpinions = AuthorizationType.no;
                dbContext.SaveChanges();
            }
        }

        public void MadeOpinionsVisible(int[] id)
        {
            for (int i = 0; i < id.Length; i++)
            {
                var option = dbContext.Opinions.Where(x => x.Id == id[i]).First();
                option.authorizationOpinions = AuthorizationType.yes;
                dbContext.SaveChanges();
            }
        }

        public int GetCountNotAuthorization()
        {
            var opinions = this.GetOpinionsForAdmin();
            var result = opinions.Length;
            return result;
        }

        public void DeleteOpinion(int id)
        {
            var opinions = dbContext.Opinions.Where(x => x.Id == id).First();
            opinions.IsDeleted = true;
            dbContext.SaveChanges();
        }

        public int Total()
        {
           var result = dbContext.Opinions
                .Where(x => x.authorizationOpinions == AuthorizationType.yes && x.IsDeleted == false)
                .Count();
            return result;
        }
    }
}

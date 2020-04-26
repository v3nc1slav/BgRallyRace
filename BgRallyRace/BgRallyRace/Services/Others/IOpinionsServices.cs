namespace BgRallyRace.Services
{
    using BgRallyRace.Models;
    using System.Threading.Tasks;

    public interface IOpinionsServices
    {
        Task<string> AddOpinionAsync(string text, string user);

        Task<Opinions[]> GetOpinionsAsync(int page = 1);

        Task<Opinions[]> GetOpinionsForAdminAsync(int page = 1);

        Task MadeOpinionsInvisibleAsync(int[] id);

        Task MadeOpinionsVisibleAsync(int[] id);

        public int GetCountNotAuthorization();

       Task DeleteOpinionAsync(int id);

        int Total();
    }
}

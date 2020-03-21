namespace BgRallyRace.Services
{
    using BgRallyRace.Models;

    public interface IOpinionsServices
    {
        void AddOpinionAsync(string text, string user);

        Opinions[] GetOpinions(int page = 1);

        Opinions[] GetOpinionsForAdmin(int page = 1);

        void MadeOpinionsInvisible(int[] id);

        void MadeOpinionsVisible(int[] id);

        public int GetCountNotAuthorization();

        void DeleteOpinion(int id);

        int Total();
    }
}

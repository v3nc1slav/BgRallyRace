namespace BgRallyRace.Services.Competitions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRaceHistoryServices
    {
        void CreateHistory(int id, string name);

        void AddHistory(string input);

        List<string> GetHistory();
    }
}

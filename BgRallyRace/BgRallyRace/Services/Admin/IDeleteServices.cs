namespace BgRallyRace.Services.Admin
{
    using System.Threading.Tasks;

    public interface IDeleteServices
    {
        Task<string> DeleteRunways(int id);

        Task<string> DeletePilots(int id);
    }
}

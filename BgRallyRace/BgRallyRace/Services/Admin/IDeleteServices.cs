namespace BgRallyRace.Services.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IDeleteServices
    {
        Task<string> DeleteRunways(int id);

        Task<string> Deletepilots(int id);
    }
}

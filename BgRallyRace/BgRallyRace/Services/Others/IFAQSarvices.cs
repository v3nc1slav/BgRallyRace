namespace BgRallyRace.Services.Others
{
    using BgRallyRace.Models.Home;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IFAQSarvices
    {
       Task<List<FAQ>> GetFAQAsync();
    }
}

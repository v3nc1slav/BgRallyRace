using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
    public interface IOpinionsServices
    {
        void AddOpinionAsync(string text, string user);
        string[] GetOpinions();

    }
}

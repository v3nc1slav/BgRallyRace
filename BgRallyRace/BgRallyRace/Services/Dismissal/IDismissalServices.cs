using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services.Dismissal
{
    public interface IDismissalServices
    {
        void DismissalPilot(int id);
        void DismissalNavigator(int id);
        void DismissalFitter(int id);

    }
}

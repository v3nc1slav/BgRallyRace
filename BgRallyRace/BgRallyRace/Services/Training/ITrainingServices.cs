using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services.Training
{
    public interface ITrainingServices
    {
        void Training(int id, string type);
        void Fitness(int id);
        void Yoga(int id);
        void TeamBuilding(int id);
        void Spa(int id);
        void Squash(int id);
        void Interview(int id);
    }
}

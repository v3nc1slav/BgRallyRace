using BgRallyRace.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services.Training
{
    public class TrainingServices : ITrainingServices
    {
        private readonly ApplicationDbContext dbContext;

        public TrainingServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Training(int id, string type)
        {

        }
        public void Fitness(int id)
        {

        }
        public void Yoga(int id)
        {

        }
        public void TeamBuilding(int id)
        {

        }
        public void Spa(int id)
        {

        }
        public void Squash(int id)
        {

        }
        public void Interview(int id)
        {

        }
    }
}

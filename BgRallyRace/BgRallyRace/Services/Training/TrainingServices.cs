namespace BgRallyRace.Services.Training
{
    using BgRallyRace.Data;
    using System.Threading.Tasks;

    public class TrainingServices : ITrainingServices
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IRallyPilotsServices pilots;
        private readonly IRallyNavigatorsServices navigators;

        public TrainingServices(ApplicationDbContext dbContext, IRallyPilotsServices pilotsServices, IRallyNavigatorsServices navigatorsServices)
        {
            this.dbContext = dbContext;
            this.pilots = pilotsServices;
            this.navigators = navigatorsServices;
        }

        public void Training(int id, string typeTreining, string typePeople)
        {
            if (typeTreining == "Fitness")
            {
                this.Fitness(id, typePeople);
            }
            else if (typeTreining == "Yoga")
            {
                this.Yoga(id, typePeople);
            }
            else if (typeTreining == "TeamBuilding")
            {
                this.TeamBuilding(id, typePeople);
            }
            else if (typeTreining == "Spa")
            {
                this.Spa(id, typePeople);
            }
            else if (typeTreining == "Sqash")
            {
                this.Squash(id, typePeople);
            }
            else if (typeTreining == "Interview")
            {
                this.Interview(id, typePeople);
            }
        }

        public void Fitness(int id, string type)
        {
            if (type == "Pilot")
            {
                pilots.DecreaseEnergy(id, 10);
                pilots.DecreasePounds(id, 1);
                pilots.IncreasePhysicalTraining(id, 2);
                pilots.IsWorking(id);
            }
            else if (type == "Navigator")
            {
                navigators.DecreaseEnergy(id, 10);
                navigators.DecreasePounds(id, 1);
                navigators.IncreasePhysicalTraining(id, 2);
                navigators.IsWorking(id);
            }
        }

        public void Yoga(int id, string type)
        {
            if (type == "Pilot")
            {
                pilots.DecreaseEnergy(id, 10);
                pilots.IncreaseConcentration(id, 2);
                pilots.IsWorking(id);
            }
            else if (type == "Navigator")
            {
                navigators.DecreaseEnergy(id, 10);
                navigators.IncreaseConcentration(id, 2);
                navigators.IsWorking(id);
            }
        }

        public void TeamBuilding(int id, string type)
        {
            if (type == "Pilot")
            {
                pilots.DecreaseEnergy(id, 10);
                pilots.IncreaseDevotion(id, 2);
                pilots.IsWorking(id);
            }
            else if (type == "Navigator")
            {
                navigators.DecreaseEnergy(id, 10);
                navigators.IncreaseDevotion(id, 2);
                navigators.IsWorking(id);
            }
        }

        public void Spa(int id, string type)
        {
            if (type == "Pilot")
            {
                pilots.IncreaseEnergy(id);
                pilots.IncreasePounds(id, 1);
                pilots.IsWorking(id);
            }
            else if (type == "Navigator")
            {
                navigators.IncreaseEnergy(id);
                navigators.IncreasePounds(id, 1);
                navigators.IsWorking(id);
            }
        }

        public void Squash(int id, string type)
        {
            if (type == "Pilot")
            {
                pilots.DecreaseEnergy(id, 15);
                pilots.IncreaseReflexes(id, 2);
                pilots.DecreasePounds(id, 1);
                pilots.IsWorking(id);
            }
        }
        
        public void Interview(int id, string type)
        {
           if (type == "Navigator")
            {
                navigators.DecreaseEnergy(id, 15);
                navigators.IncreaseCommunication(id, 3);
                navigators.IsWorking(id);
           }
        }
    }
}

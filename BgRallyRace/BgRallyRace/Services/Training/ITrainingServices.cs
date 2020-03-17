namespace BgRallyRace.Services.Training
{
    public interface ITrainingServices
    {
        void Training(int id, string type, string typeP);

        void Fitness(int id, string type);

        void Yoga(int id, string type);

        void TeamBuilding(int id, string type);

        void Spa(int id, string type);

        void Squash(int id, string type);

        void Interview(int id, string type);
    }
}

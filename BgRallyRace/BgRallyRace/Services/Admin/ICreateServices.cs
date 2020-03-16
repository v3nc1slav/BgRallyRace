namespace BgRallyRace.Services.Admin
{
    using BgRallyRace.Models.Enums;

    public interface ICreateServices
    {
        void CreateRunwayServices(string name, decimal length, DifficultyType difficulty, string description);
        void CreatePilotServices(string? firstName, string? lastName, int age, int concentration, int experience,
            int energy, int devotion, int physicalTraining, int pounds, int salary, int reflexes);
    }
}

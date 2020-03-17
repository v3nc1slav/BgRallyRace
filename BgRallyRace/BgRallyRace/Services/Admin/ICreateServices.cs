namespace BgRallyRace.Services.Admin
{
    using BgRallyRace.Models.Enums;

    public interface ICreateServices
    {
        void CreateRunway(string name, decimal length, DifficultyType difficulty, string description);
        void CreatePilot(string? firstName, string? lastName, int age, int concentration, int experience,
            int energy, int devotion, int physicalTraining, int pounds, int salary, int reflexes);

        void CreateNavigator(string? firstName, string? lastName, int age, int concentration, int experience,
            int energy, int devotion, int physicalTraining, int pounds, int salary, int reflexes);
    }
}

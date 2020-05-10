namespace BgRallyRace.Services
{
    using BgRallyRace.Models;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Models.PartsCar;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface ICarServices
    {
        Task<int> GetCarId(string user);

        Task<List<Cars>> GetCars(string user);

        Task<Cars> GetCar(string user);

        Task<Cars> GetCar(int id);

        Task<Aerodynamics> GetAerodynamics(string user);

        Task<Aerodynamics> GetAerodynamics(int id);

        Brakes GetBrakes(string user);

        Engines GetEngine(string user);

        Gearboxs GetGearboxs(string user);

        ModelsCars GetModelsCars(string user);

        Mountings GetMountings(string user);

        Turbo? GetTurbo(string user);

        decimal GetMaxSpeed(string user);

        void GetNewAerodynamics(PartsCars part, Cars car);

        void GetNewBrakes(PartsCars part, Cars car);

        void GetNewEngine(PartsCars part, Cars car);

        void GetNewGearbox(PartsCars part, Cars car);

        void GetNewModelsCar(PartsCars part, Cars car);

        void GetNewMountings(PartsCars part, Cars car);

        void GetNewTurbo(PartsCars part, Cars car);

        Task<Turbo> GetTurboId(int? id);

        decimal GetCurrentSpeed(Parts parts);

        Task<int> CreateCarsAsync();

        Task<string> Repair(string type, int id, decimal price, string user);

        void Damage(int carId, int typeDamage, DifficultyType difficulty);

        decimal GetMaxCurrentSpeed(string user);
    }
}

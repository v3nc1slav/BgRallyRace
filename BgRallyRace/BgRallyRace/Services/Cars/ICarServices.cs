namespace BgRallyRace.Services
{
    using BgRallyRace.Models;
    using BgRallyRace.Models.PartsCar;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface ICarServices
    {
        int GetCarId(string user);

        List<Cars> GetCars(string user);

        Cars GetCar(int id);

        Cars GetCar(string user);

        Aerodynamics GetAerodynamics(string user);

        Brakes GetBrakes(string user);

        Engines GetEngine(string user);

        Gearboxs GetGearboxs(string user);

        ModelsCars GetModelsCars(string user);

        Mountings GetMountings(string user);

        Turbo? GetTurbo(string user);

        decimal GetMaxSpeed(string user);

        void GetNewEngine(string user);

        decimal GetCurrentSpeed(Parts parts);

        int CreateCarsAsync();

        void Repair(string type, int id);
    }
}

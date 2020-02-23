using BgRallyRace.Models;
using BgRallyRace.Models.PartsCar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
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
        decimal GetMaxSpeed(Aerodynamics aer, Brakes br, Engines en, Gearboxs gb,
            ModelsCars mc, Mountings mou, Turbo tur);
        void GetNewEngine(string user);
        decimal GetCurrentSpeed(Parts parts);
        int CreateCarsAsync();

    }
}
